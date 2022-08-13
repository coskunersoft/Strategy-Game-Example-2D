using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.GridSystem;
using AOP.EventFactory;
using AOP.DataCenter;
using UnityEngine.SceneManagement;
using AOP.Management.Scene;
using AOP.ObjectPooling;

namespace AOP.GamePlay.Squance
{
    public class GameSquance 
    {
        private GameGrid GameGrid;
        private bool GameSceneLoaded;
        private bool EnvironmentLoaded;
        private InGameEnvironment InGameEnvironmentObject;
        private GameLevelSO currentLevelSO;
        private GridConfigurationSO gridConfigurationSO;
        private List<IGameSquanceJob> jobs;

        public IEnumerator Load(GameLevelSO gameLevelSO)
        {
            jobs = new List<IGameSquanceJob>();
            currentLevelSO = gameLevelSO;
            gridConfigurationSO = ObjectCamp.PullScriptable<GridConfigurationSO>();
            Events.GeneralEvents.OnLevelLoadingStarted?.Invoke(gameLevelSO);
            yield return new WaitUntil(() => GameSceneLoaded);
            LoadInGameEnvironment();
            yield return new WaitUntil(() => EnvironmentLoaded);
        }
        private async void LoadInGameEnvironment()
        {
            var SpawnEnvironmentTask = ObjectCamp.PullObject<InGameEnvironment>(PoolStaticVariations.VARIATION1);
            await SpawnEnvironmentTask;
            InGameEnvironmentObject = SpawnEnvironmentTask.Result;

            Vector2 startPosition = InGameEnvironmentObject.GridStartPoint.position;
            float gridWitdh = currentLevelSO.GridSize * gridConfigurationSO.GridCellDistance;
            startPosition -= (0.5f*gridWitdh) * (Vector2.up + Vector2.right);

            Vector4 CameraLimits = new Vector4(startPosition.x, startPosition.x + gridWitdh, startPosition.y, startPosition.y + gridWitdh);
            InGameEnvironmentObject.CameraControl.Init(CameraLimits, InGameEnvironmentObject.GridStartPoint.position);

            GameGrid = new GameGrid(gridConfigurationSO, currentLevelSO.GridSize, startPosition);
            EnvironmentLoaded = true;
        }

        public void SubscribeEvents()
        {
            Events.SceneEvents.OnAnyMasterSceneLoadingCompeted += OnAnyMasterSceneLoadingCompeted;
            Events.GamePlayEvents.OnAnyGridCellClicked += OnAnyGridCellClicked;
            Events.GamePlayEvents.OnAnyBuildDraggedFromMenu += OnAnyBuildDraggedFromMenu;
        }
        public void UnSubscribeEvents()
        {
            Events.SceneEvents.OnAnyMasterSceneLoadingCompeted -= OnAnyMasterSceneLoadingCompeted;
            Events.GamePlayEvents.OnAnyGridCellClicked -= OnAnyGridCellClicked;
            Events.GamePlayEvents.OnAnyBuildDraggedFromMenu -= OnAnyBuildDraggedFromMenu;

        }

        public void GameUpdate()
        {
            ExacuteJobs();
        }

        private void ExacuteJobs()
        {
            for (int i = 0; i < jobs.Count; i++)
            {
                var job = jobs[i];
                if (job.IsStarted)
                {
                    if (job.CompleteRule())
                    {
                        job.Completed();
                        jobs.Remove(job);
                    }
                    else
                        job.Continue();

                }
                else
                    job.StartJob();
            }
        }
       

        #region EventListener
        private void OnAnyMasterSceneLoadingCompeted(MasterSceneType masterSceneType)
        {
            GameSceneLoaded = masterSceneType == MasterSceneType.Game;
        }
        private void OnAnyGridCellClicked(GridCell cell)
        {
            Debug.Log(cell.cellCoordinate);
        }
        private void OnAnyBuildDraggedFromMenu(BuildingSO buildingSO)
        {
            Debug.Log(buildingSO.UnitName+" Trying to Drag");
            var job = new BuildPlaceJob(GameGrid,buildingSO);
            jobs.Add(job);
        }
        #endregion
    }
}
