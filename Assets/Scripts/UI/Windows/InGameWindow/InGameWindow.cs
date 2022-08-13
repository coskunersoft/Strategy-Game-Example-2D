using System.Collections;
using System.Collections.Generic;
using AOP.ObjectPooling;
using UnityEngine;
using AOP.DataCenter;

namespace AOP.UI.Windows
{
    public class InGameWindow : IUIWindow
    {
        protected override IEnumerator ShowSquence(bool hitEvent = false, bool animated = true)
        {
            yield return base.ShowSquence(hitEvent, animated);

            var GameDataSO = ObjectCamp.PullScriptable<GameDataSO>();
            var LeftWindow = GetSubWindow<LeftPanelWindow>();
            LeftWindow.Show();
            LeftWindow.ApplyData(GameDataSO);
        }
    }

}
