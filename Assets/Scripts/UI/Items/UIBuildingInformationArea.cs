using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using AOP.UI.DataContainers;
using AOP.DataCenter;
using AOP.GamePlay.DataMaps;
using AOP.UI.Items;
using AOP.ObjectPooling;

namespace AOP.UI.Items
{
    public class UIBuildingInformationArea : IUIDisplayable,IUIDataContainer<BuildingInformationData>
    {
        public TextMeshProUGUI TextBuildingName;
        public TextMeshProUGUI TextBuildingDesc;
        public Image ImageBuildingIcon;

        public CanvasGroup ProductionAreaCanvasGroup;
        public Transform BuildingProductionItemsCarrier;

        private List<UIBuildingProductionItem> productionItems;

        public void ApplyData(BuildingInformationData Data)
        {
            ProductionAreaCanvasGroup.alpha = 0f;
            ProductionAreaCanvasGroup.interactable = false;
            ProductionAreaCanvasGroup.blocksRaycasts = false;
            TextBuildingName.text = Data.buildingSO.UnitName;
            TextBuildingDesc.text = Data.buildingSO.UnitDesc;
            ImageBuildingIcon.sprite = Data.buildingSO.UnitIcon;
            if (Data.buildingSO is BarrackBuildingSO barrackBuildingSO)
            {
                productionItems ??= new List<UIBuildingProductionItem>();
                foreach (var item in productionItems)
                {
                    ObjectCamp.PushObject(item);
                }
                productionItems.Clear();

                foreach (var item in barrackBuildingSO.ProductionMilitaryUnits)
                {
                    CreateProductionItem(item);
                }
                if (Data.gameBuildingUnit != null)
                {
                    ProductionAreaCanvasGroup.alpha = 1;
                    ProductionAreaCanvasGroup.interactable = true;
                    ProductionAreaCanvasGroup.blocksRaycasts = true;
                }
            }
            
            
        }

        private async void CreateProductionItem(MilitaryUnitSO militaryUnitSO)
        {
            var task = ObjectCamp.PullObject<UIBuildingProductionItem>(carrier: BuildingProductionItemsCarrier);
            await task;
            task.Result.ApplyData(militaryUnitSO);
            productionItems.Add(task.Result);
        }

        private void OnDestroy()
        {
            productionItems ??= new List<UIBuildingProductionItem>();
            foreach (var item in productionItems)
            {
                ObjectCamp.PushObject(item);
            }
        }
    }
}
