using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.UI.DataContainers;
using AOP.DataCenter;
using AOP.ObjectPooling;
using AOP.UI.Items;
using AOP.GamePlay.Units;
using AOP.EventFactory;
using AOP.GamePlay.DataMaps;

namespace AOP.UI.Windows
{
    public class RightPanelWindow : IUISubWindow,IObjectCampMember
    {
        public UIBuildingInformationArea UIBuildingInformationArea;

        #region Mono Functions
        private void OnEnable()
        {
            Events.UIEvents.OnGameWindowBuildingClick += OnGameWindowBuildingClick;
            Events.GamePlayEvents.OnAnyUnitSelectedInGameArea += OnAnyUnitSelectedInGameArea;
        }
        private void OnDisable()
        {
            Events.UIEvents.OnGameWindowBuildingClick -= OnGameWindowBuildingClick;
            Events.GamePlayEvents.OnAnyUnitSelectedInGameArea+=OnAnyUnitSelectedInGameArea;
        }
        #endregion

        #region Event Listeners
        private void OnGameWindowBuildingClick(BuildingSO buildingSO)
        {
            UIBuildingInformationArea.Show(hitEvent:false);
            UIBuildingInformationArea.ApplyData(new BuildingInformationData() {buildingSO=buildingSO, gameBuildingUnit=null });
        }
        private void OnAnyUnitSelectedInGameArea(IGameUnit gameUnit)
        {
            if (gameUnit is IGameBuildingUnit gameBuildingUnit)
            {
                UIBuildingInformationArea.Show(hitEvent: false);
                UIBuildingInformationArea.ApplyData(new BuildingInformationData() { buildingSO = gameBuildingUnit.buildingSO, gameBuildingUnit = gameBuildingUnit });
            }
        }
        #endregion


    }
}