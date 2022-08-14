using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AOP.UI.DataContainers;
using AOP.DataCenter;
using AOP.ObjectPooling;
using UnityEngine.EventSystems;
using AOP.EventFactory;

namespace AOP.UI.Items
{
    public class UIBuildingProductionItem : MonoBehaviour, IUIDataContainer<MilitaryUnitSO>, IObjectCampMember
    {
        public Image ProductionImage;
        public Button ProductionButton;

        public void ApplyData(MilitaryUnitSO Data)
        {
            ProductionImage.sprite = Data.UnitIcon;
            ProductionButton.onClick = new Button.ButtonClickedEvent();
            ProductionButton.onClick.AddListener(OnClick);
        }
        private void OnClick()
        {

        }
    }
}

