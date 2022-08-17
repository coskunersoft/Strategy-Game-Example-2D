using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using AOP.UI.DataContainers;
using AOP.DataCenter;
using AOP.GamePlay.DataMaps;
using AOP.GamePlay.Units;
using AOP.ObjectPooling;
using AOP.Extensions;
using AOP.EventFactory;
using AOP.GamePlay.ChangeableSystems;
using System.Collections;

namespace AOP.UI.Items
{
    public class UIUnitInformationArea : IUIDisplayable,IUIDataContainer<BuildingInformationData>
    {
        private BuildingInformationData buildingInformationData;

        public TextMeshProUGUI TextBuildingName;
        public TextMeshProUGUI TextBuildingDesc;
        public Image ImageBuildingIcon;

        public CanvasGroup ProductionAreaCanvasGroup;
        public CanvasGroup ProductionAreaItemListCanvasGroup;
        public CanvasGroup ProductionAreaInProgressCanvasGroup;
        public Transform BuildingProductionItemsCarrier;
        public Image ProductingItemImage;
        public UIProgressBar ProductingItemProgressbar;

        private List<UIBuildingProductionItem> productionItems;

        private void OnEnable()
        {
            Events.GamePlayEvents.OnAnyBarrackBuildingStartedProducting += OnAnyBarrackBuildingStartedProducting;
            Events.GamePlayEvents.OnAnyBarrackBuildingProductingProgress += OnAnyBarrackBuildingProductingProgress;
            Events.GamePlayEvents.OnAnyBarrackBuildingFinishProducting += OnAnyBarrackBuildingFinishProducting;


        }
        private void OnDisable()
        {
            Events.GamePlayEvents.OnAnyBarrackBuildingStartedProducting -= OnAnyBarrackBuildingStartedProducting;
            Events.GamePlayEvents.OnAnyBarrackBuildingProductingProgress -= OnAnyBarrackBuildingProductingProgress;
            Events.GamePlayEvents.OnAnyBarrackBuildingFinishProducting -= OnAnyBarrackBuildingFinishProducting;

        }

        protected override IEnumerator HideSquence(bool hitEvent = false, bool animated = true)
        {
            if (buildingInformationData != null)
            {
                if (buildingInformationData.gameUnit != null)
                {
                    buildingInformationData.gameUnit.HealthSystem.onAmountEmpty -= OnDisplayingGameUnitDead;
                    buildingInformationData.gameUnit.HealthSystem.onAmountChanged -= OnDisplayingGameUnitsHealthChanged;
                }
            }
            yield return base.HideSquence(hitEvent, animated);
        }

        public void ApplyData(BuildingInformationData Data)
        {
            if (buildingInformationData != null)
            {
                if (buildingInformationData.gameUnit != null)
                {
                    buildingInformationData.gameUnit.HealthSystem.onAmountEmpty -= OnDisplayingGameUnitDead;
                    buildingInformationData.gameUnit.HealthSystem.onAmountChanged -= OnDisplayingGameUnitsHealthChanged;
                }
            }


            buildingInformationData = Data;
            ProductionAreaCanvasGroup.ShowHideCanvasGroup(false);
            ProductionAreaItemListCanvasGroup.ShowHideCanvasGroup(false);
            ProductionAreaInProgressCanvasGroup.ShowHideCanvasGroup(false);
            TextBuildingName.text = Data.gameUnitSO.UnitName;
            ImageBuildingIcon.sprite = Data.gameUnitSO.UnitIcon;
            UpdateDescription();
            

            if (Data.gameUnit != null)
            {
                Data.gameUnit.HealthSystem.onAmountEmpty += OnDisplayingGameUnitDead;
                Data.gameUnit.HealthSystem.onAmountChanged += OnDisplayingGameUnitsHealthChanged;
                if (Data.gameUnitSO is BarrackBuildingSO barrackBuildingSO)
                {
                    if (Data.gameUnit is IGameBarrackBuildingUnit barrackBuildingUnit)
                    {
                        ProductionAreaCanvasGroup.ShowHideCanvasGroup(true);
                        if (!barrackBuildingUnit.currentProductionUnit)
                        {
                            productionItems ??= new List<UIBuildingProductionItem>();
                            foreach (var item in productionItems)
                                ObjectCamp.PushObject(item);

                            productionItems.Clear();

                            foreach (var item in barrackBuildingSO.ProductionMilitaryUnits)
                                CreateProductionItem(item, barrackBuildingUnit);
                            
                            ProductionAreaItemListCanvasGroup.ShowHideCanvasGroup(true);
                        }
                        else
                        {
                            ProductionAreaItemListCanvasGroup.ShowHideCanvasGroup(false);
                            ProductionAreaInProgressCanvasGroup.ShowHideCanvasGroup(true);
                            OnProductionProgressUpdate();
                        }
                    }
                }
            }
            
        }
        private async void CreateProductionItem(MilitaryUnitSO militaryUnitSO, IGameBarrackBuildingUnit gameBarrackBuildingUnit)
        {
            var task = ObjectCamp.PullObject<UIBuildingProductionItem>(carrier: BuildingProductionItemsCarrier);
            await task;
            task.Result.ApplyData(new MilitaryProductionItemData() { gameBarrackBuildingUnit = gameBarrackBuildingUnit, militaryUnitSO = militaryUnitSO });
            productionItems.Add(task.Result);
        }

        private void OnProductionProgressUpdate()
        {
            if (buildingInformationData==null) return;
            if (!buildingInformationData.gameUnit) return;

            if (buildingInformationData.gameUnit is IGameBarrackBuildingUnit barrackBuildingUnit)
            {
                ProductingItemImage.sprite = barrackBuildingUnit.currentProductionUnit.UnitIcon;
                ProductingItemProgressbar.SetProgress(barrackBuildingUnit.ProductionProgressTime);
            }
        }
        private void UpdateDescription()
        {
            TextBuildingDesc.text = "<i>"+buildingInformationData.gameUnitSO.UnitDesc+"</i>";
            string extraDesc = "\n";
            if (buildingInformationData.gameUnit != null)
            {
                extraDesc += "<b>Health</b> : " + buildingInformationData.gameUnit.HealthSystem.Amount + "/" + buildingInformationData.gameUnit.HealthSystem.MaxAmount;
                if(buildingInformationData.gameUnit is IGameMilitaryUnit gameMilitaryUnit)
                {
                    extraDesc += "\n <b>Strike : </b>"+gameMilitaryUnit.militaryUnitSO.StrikePower;
                }
              
            }
            else
            {
                extraDesc += "<b>Health</b> : " + buildingInformationData.gameUnitSO.MaxHealth + "/" + buildingInformationData.gameUnitSO.MaxHealth;
            }
            TextBuildingDesc.text += extraDesc;
        }

        #region Event Listeners

        private void OnDisplayingGameUnitDead(IDamage damge)
        {
            Hide(false, false);
        }
        private void OnDisplayingGameUnitsHealthChanged()
        {
            UpdateDescription();
        }

        private void OnAnyBarrackBuildingStartedProducting(IGameBarrackBuildingUnit gameBarrackBuildingUnit, MilitaryUnitSO militaryUnitSO)
        {
            if (buildingInformationData == null) return;
            if (buildingInformationData.gameUnit != gameBarrackBuildingUnit) return;
            ApplyData(buildingInformationData);
        }

        private void OnAnyBarrackBuildingProductingProgress(IGameBarrackBuildingUnit gameBarrackBuildingUnit, MilitaryUnitSO militaryUnitSO)
        {
            if (buildingInformationData == null) return;
            if (buildingInformationData.gameUnit != gameBarrackBuildingUnit) return;
            OnProductionProgressUpdate();
        }
        
        private void OnAnyBarrackBuildingFinishProducting(IGameBarrackBuildingUnit gameBarrackBuildingUnit, IGameMilitaryUnit gameMilitaryUnit)
        {
            if (buildingInformationData == null) return;
            if (buildingInformationData.gameUnit != gameBarrackBuildingUnit) return;
            ApplyData(buildingInformationData);
        }
        #endregion
    }
}
