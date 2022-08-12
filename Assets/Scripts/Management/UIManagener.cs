using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.EventFactory;
using AOP.UI.Windows;
using AOP.ObjectPooling;
using AOP.UI;

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

        private void OnEnable()
        {
            Events.General.OnGameInitializationStep += OnGameInitializationStep;
        }
        private void OnDisable()
        {
            Events.General.OnGameInitializationStep -= OnGameInitializationStep;
        }

        private void OnGameInitializationStep(int step)
        {
            switch (step)
            {

                case GameInitiazationSteps.GameAwake:
                    ShowHideLoadingWindow(true);
                    break;
                case GameInitiazationSteps.ShowEntiyWindow:
                    LoadMasterWindow(WindowTitles.EntryWindow);
                    break;
                case GameInitiazationSteps.EndEntityWindow:
                    
                    break;
            }
        }

        private void ShowHideLoadingWindow(bool status)
        {
            if (status)
            {
                if (loadingWindow.isDisplaying) return;
                loadingWindow.Show(hitEvent:true, animated:false);
            }
            else
            {
                loadingWindow.Hide(hitEvent: true, animated:false);
            }
        }

        private async void LoadMasterWindow(string WindowTitle) 
        {
            ShowHideLoadingWindow(true);
            if (currentMasterWindow)
            {
                currentMasterWindow.Hide();
                currentMasterWindow = null;
            }
            var task= ObjectCamp.PullObject<IUIWindow>(variation:WindowTitle);
            await task;
            currentMasterWindow = task.Result;
            currentMasterWindow.Show(true);
            ShowHideLoadingWindow(false);
        }
    }
}

