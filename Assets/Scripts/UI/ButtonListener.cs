using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.EventFactory;

namespace AOP.UI
{
    public class ButtonListener : MonoBehaviour
    {
        private void AnyButtonClick()
        {
            Events.UIEvents.OnAnyButtonClick?.Invoke();
        }
        public void MainMenuButtons(int ID)
        {
            switch (ID)
            {
                case 1://New Game
                    Events.UIEvents.OnMainMenuNewGameButtonClick?.Invoke();
                    break;
                case 2://Exit
                    Events.UIEvents.OnMainMenuExitButtonClick?.Invoke();
                    Application.Quit();
                    break;
                case 3://Back in New Game Menu
                    Events.UIEvents.OnNewGameMenuBackButtonClick?.Invoke();
                    break;
            }
        }
    }
}

