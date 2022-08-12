using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.GridSystem;
using AOP.EventFactory;
using AOP.DataCenter;
using UnityEngine.SceneManagement;
using AOP.Management.Scene;
using AOP.ObjectPooling;

namespace AOP.GamePlay
{
    public class GameSquance
    {
        private GameGrid GameGrid;
        private bool GameSceneLoaded;
        private InGameEnvironment InGameEnvironmentObject;
        private GameLevelSO currentLevelSO;
        private GridConfigurationSO gridConfigurationSO;

        public GameSquance()
        {
            Events.SceneEvents.OnAnyMasterSceneLoadingCompeted += OnAnyMasterSceneLoadingCompeted;
        }
        ~GameSquance()
        {
            Events.SceneEvents.OnAnyMasterSceneLoadingCompeted -= OnAnyMasterSceneLoadingCompeted;
        }
        public IEnumerator Init(GameLevelSO gameLevelSO)
        {
            currentLevelSO = gameLevelSO;
            gridConfigurationSO = ObjectCamp.PullScriptable<GridConfigurationSO>();
            Events.GeneralEvents.OnLevelLoadingStarted?.Invoke(gameLevelSO);
            yield return new WaitUntil(() => GameSceneLoaded);
            CreateInGameEnvironment();
            yield return null;
        }

        private async void CreateInGameEnvironment()
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
        }
        
        #region EventListener
        public void OnAnyMasterSceneLoadingCompeted(MasterSceneType masterSceneType)
        {
            GameSceneLoaded = masterSceneType == MasterSceneType.Game;
        }
        #endregion
    }
}
