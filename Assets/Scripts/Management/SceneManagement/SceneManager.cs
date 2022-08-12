using System.Collections;
using System.Collections.Generic;
using AOP.Management.Scene;
using AOP.ObjectPooling;
using UnityEngine;
using AOP.DataCenter;
using AOP.EventFactory;
using UnityEngine.SceneManagement;
using SceneOfficer = UnityEngine.SceneManagement.SceneManager;

namespace AOP.Management
{
    public class SceneManager : IManager
    {
        public override IEnumerator Init()
        {

            yield return null;
        }

        private void OnEnable()
        {
            SceneOfficer.sceneLoaded += SceneOfficer_SceneLoaded;
            Events.GeneralEvents.OnGameInitializationStep += OnGameInitializationStep;
        }
        private void OnDisable()
        {
            SceneOfficer.sceneLoaded -= SceneOfficer_SceneLoaded;
        }
        public void LoadScene(MasterSceneType masterSceneType)
        {
            var sceneDataSO = ObjectCamp.PullScriptable<SceneDataSO>();
            MasterSceneMap masterSceneMap = sceneDataSO.masterSceneMaps.Find(x => x.sceneType == masterSceneType);
            SceneOfficer.LoadScene(masterSceneMap.SceneName, LoadSceneMode.Single);
            Events.SceneEvents.OnAnyMasterSceneLoadingStarted?.Invoke(masterSceneType);
        }
        private void SceneOfficer_SceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
        {
            var sceneDataSO = ObjectCamp.PullScriptable<SceneDataSO>();
            if (loadSceneMode == LoadSceneMode.Single)
            {
                MasterSceneMap masterSceneMap = sceneDataSO.masterSceneMaps.Find(x => x.SceneName == scene.name);
                Events.SceneEvents.OnAnyMasterSceneLoadingCompeted?.Invoke(masterSceneMap.sceneType);
            }
        }
        private void OnGameInitializationStep(int step)
        {
            switch (step)
            {
                case GameInitiazationSteps.GameInitializationDone:
                    LoadScene(MasterSceneType.Menu);
                    break;
            }
        }
    }
}
