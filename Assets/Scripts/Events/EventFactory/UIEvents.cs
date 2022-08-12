using System;
using AOP.DataCenter;
using AOP.UI.Windows;
using UnityEngine;

namespace AOP.EventFactory
{
    public static partial class Events
    {
        public static class UIEvents
        {
           
            public static Action<IUIWindow> OnAnyMasterWindowShow;
            public static Action<IUIWindow> OnAnyMasterWindowHide;
            public static Action<IUISubWindow> OnAnySubWindowHide;
            public static Action<IUISubWindow> OnAnySubWindowShow;

            public static Action OnAnyButtonClick;
            public static Action OnMainMenuNewGameButtonClick;
            public static Action OnMainMenuExitButtonClick;
            public static Action OnNewGameMenuBackButtonClick;

            public static Action<GameLevelSO> OnGameLevelSelectedButtonClick;
        }
    }
}

