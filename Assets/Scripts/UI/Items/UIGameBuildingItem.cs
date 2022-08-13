using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AOP.UI.DataContainers;
using AOP.DataCenter;
using AOP.ObjectPooling;
using UnityEngine.EventSystems;
using AOP.EventFactory;

namespace AOP.UI
{
    public class UIGameBuildingItem : MonoBehaviour, IUIDataContainer<BuildingSO>,IObjectCampMember
    {
        private BuildingSO buildingSO;
        public Image buildingIcon;

        public void ApplyData(BuildingSO Data)
        {
            buildingSO = Data;
            buildingIcon.sprite = Data.UnitIcon;
        }

        public void OnBeginDrag()
        {
            Events.GamePlayEvents.OnAnyBuildDraggedFromMenu?.Invoke(buildingSO);
        }
    }
}
