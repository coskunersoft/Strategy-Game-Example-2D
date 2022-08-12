using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.EventFactory;

namespace AOP.UI.Windows
{
    public class MainMenuWindow : IUIWindow
    {
        private void OnEnable()
        {
            Events.UIEvents.OnMainMenuNewGameButtonClick += OnMainMenuNewGameButtonClick;
            Events.UIEvents.OnNewGameMenuBackButtonClick += OnNewGameMenuBackButtonClick;
        }
        private void OnDisable()
        {
            Events.UIEvents.OnMainMenuNewGameButtonClick -= OnMainMenuNewGameButtonClick;
            Events.UIEvents.OnNewGameMenuBackButtonClick -= OnNewGameMenuBackButtonClick;


        }

        protected override IEnumerator ShowSquence(bool hitEvent = false, bool animated = true)
        {
           yield return base.ShowSquence(hitEvent, animated);
           
        }

        #region Event Listeners
        private void OnMainMenuNewGameButtonClick()
        {
            DisplaySubWindowWithType<NewGameWindow>();
        }
        private void OnNewGameMenuBackButtonClick()
        {
            var Window = GetSubWindow<NewGameWindow>();
            Window.Hide(true,true);
        }
        #endregion

    }
}
