using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.UI.DataContainers;
using AOP.DataCenter;
using AOP.ObjectPooling;
using UnityEngine.UI;
using AOP.Tools;

namespace AOP.UI.Windows
{
    public class LeftPanelWindow : IUISubWindow, IUIDataContainer<GameDataSO>
    {
        private GameDataSO currentData;
        [SerializeField]private Transform buildingItemCarrier;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private InfiniteScroll InfiniteScroll;

        private List<UIGameBuildingItem> currentUIGameBuildingItems;

       
        public void ApplyData(GameDataSO Data)
        {
            currentData = Data;
            currentUIGameBuildingItems ??= new List<UIGameBuildingItem>();
            foreach (var item in currentUIGameBuildingItems)
                ObjectCamp.PushObject(item);

            for (int i = 0; i < 5; i++)
            foreach (var item in Data.allBuildings)
                CreateUIBuildingItem(item);

            StartCoroutine(InitWaiter());
        }

        IEnumerator InitWaiter()
        {
            yield return new WaitUntil(() => currentUIGameBuildingItems.Count >= currentData.allBuildings.Count);
            InfiniteScroll.Init();
        }

        private async void CreateUIBuildingItem(BuildingSO buildingSO)
        {
            var task = ObjectCamp.PullObject<UIGameBuildingItem>(carrier: buildingItemCarrier);
            await task;
            task.Result.ApplyData(buildingSO);
            currentUIGameBuildingItems.Add(task.Result);
        }
    }
}