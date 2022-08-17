using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AOP.UI.DataContainers;
using AOP.DataCenter;
using AOP.ObjectPooling;
using AOP.GamePlay.DataMaps;
using AOP.EventFactory;

namespace AOP.UI.Items
{
    public class UIBuildingProductionItem : MonoBehaviour, IUIDataContainer<MilitaryProductionItemData>, IObjectCampMember
    {
        public Image ProductionImage;
        public Button ProductionButton;
        private MilitaryProductionItemData MilitaryProductionItemData;

        public void ApplyData(MilitaryProductionItemData Data)
        {
            this.MilitaryProductionItemData = Data;
            ProductionImage.sprite = Data.militaryUnitSO.UnitIcon;
            ProductionButton.onClick = new Button.ButtonClickedEvent();
            ProductionButton.onClick.AddListener(OnClick);
        }
        private void OnClick()
        {
            Events.GamePlayEvents.OnAnyBarrackProductionCreateRequest?.Invoke(MilitaryProductionItemData.gameBarrackBuildingUnit, MilitaryProductionItemData.militaryUnitSO);
        }
    }
}

