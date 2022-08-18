using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.EventFactory;
using AOP.UI.Windows;
using AOP.ObjectPooling;
using AOP.UI;
using AOP.Management.Scene;

namespace AOP.Management
{
    public class UIManagener : IManager
    {
        [SerializeField]private LoadingWindow loadingWindow;
        private IUIWindow currentMasterWindow;

        public override IEnumerator Init()
        {
            ShowHideLoadingWindow(true);
            yield return null;
        }

        #region Mono Functions
        private void OnEnable()
        {
            Events.GeneralEvents.OnGameInitializationStep += OnGameInitializationStep;
            Events.SceneEvents.OnAnyMasterSceneLoadingStarted += OnAnyMasterSceneLoadingStarted;
            Events.SceneEvents.OnAnyMasterSceneLoadingCompeted += OnAnyMasterSceneLoadingCompeted;
        }
        private void OnDisable()
        {
            Events.GeneralEvents.OnGameInitializationStep -= OnGameInitializationStep;
            Events.SceneEvents.OnAnyMasterSceneLoadingStarted -= OnAnyMasterSceneLoadingStarted;
            Events.SceneEvents.OnAnyMasterSceneLoadingCompeted -= OnAnyMasterSceneLoadingStarted;
        }
        #endregion

        #region UI Manager Bussines
        private void ShowHideLoadingWindow(bool status)
        {
            if (status)
            {
                if (loadingWindow.isDisplaying) return;
                loadingWindow.Show(hitEvent: true, animated: false);
            }
            else
            {
                loadingWindow.Hide(hitEvent: true, animated: false);
            }
        }
        private async void LoadMasterWindow(string WindowTitle)
        {
            if (currentMasterWindow)
            {
                currentMasterWindow.Hide();
                currentMasterWindow = null;
            }
            var task = ObjectCamp.PullObject<IUIWindow>(variation: WindowTitle);
            await task;
            currentMasterWindow = task.Result;
            currentMasterWindow.Show(true);
        }
        #endregion

        #region Event Listeners
        private void OnGameInitializationStep(int step)
        {
            switch (step)
            {
                case GameInitiazationSteps.GameAwake:
                    ShowHideLoadingWindow(true);
                    break;
                case GameInitiazationSteps.ShowEntryWindow:
                    LoadMasterWindow(WindowTitles.EntryWindow);
                    break;
            }
        }
        private void OnAnyMasterSceneLoadingStarted(MasterSceneType masterSceneType)
        {
            ShowHideLoadingWindow(true);
        }
        private void OnAnyMasterSceneLoadingCompeted(MasterSceneType masterSceneType)
        {
            ShowHideLoadingWindow(false);
            switch (masterSceneType)
            {
                case MasterSceneType.Menu:
                    LoadMasterWindow(WindowTitles.MainMenuWindow);
                    break;
                case MasterSceneType.Game:
                    LoadMasterWindow(WindowTitles.InGameWindow);
                    break;
            }
        }
        #endregion


    }
}

