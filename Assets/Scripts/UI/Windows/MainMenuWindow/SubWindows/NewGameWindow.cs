using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.UI.DataContainers;
using AOP.DataCenter;
using AOP.ObjectPooling;
using AOP.UI.Items;

namespace AOP.UI.Windows
{
    public class NewGameWindow : IUISubWindow, IUIDataContainer<GameDataSO>
    {
        public Transform UILevelItemCarrier;
        private List<UIGameLevelItem> uIGameLevelItems;

        #region Window Bussiness
        public void ApplyData(GameDataSO Data)
        {
            foreach (var item in Data.gameLevels)
                MakeLevelItem(item);
        }
        private async void MakeLevelItem(GameLevelSO gameLevelSO)
        {
            var task = ObjectCamp.PullObject<UIGameLevelItem>(carrier: UILevelItemCarrier);
            await task;
            task.Result.ApplyData(gameLevelSO);
            uIGameLevelItems.Add(task.Result);
        }

        protected override IEnumerator ShowSquence(bool hitEvent = false, bool animated = true)
        {
            uIGameLevelItems ??= new List<UIGameLevelItem>();
            yield return base.ShowSquence(hitEvent, animated);
            var gameDataSO = ObjectCamp.PullScriptable<GameDataSO>();
            ApplyData(gameDataSO);
        }
        protected override IEnumerator HideSquence(bool hitEvent = false, bool animated = true)
        {
            yield return base.HideSquence(hitEvent, animated);

            foreach (var item in uIGameLevelItems)
            {
                ObjectCamp.PushObject(item);
            }
            uIGameLevelItems.Clear();
        }
        #endregion
    }
}
