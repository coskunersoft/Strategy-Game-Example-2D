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
using AOP.GridSystem;

namespace AOP.UI.Windows
{
    public class RightPanelWindow : IUISubWindow,IObjectCampMember
    {
        public UIUnitInformationArea UIBuildingInformationArea;

        #region Mono Functions
        private void OnEnable()
        {
            Events.UIEvents.OnGameWindowBuildingClick += OnGameWindowBuildingClick;
            Events.GamePlayEvents.OnAnyUnitSelectedInGameArea += OnAnyUnitSelectedInGameArea;
            Events.GamePlayEvents.OnAnyGridCellMouseOneClicked += OnAnyGridCellMouseOneClicked;
        }
        private void OnDisable()
        {
            Events.UIEvents.OnGameWindowBuildingClick -= OnGameWindowBuildingClick;
            Events.GamePlayEvents.OnAnyUnitSelectedInGameArea-=OnAnyUnitSelectedInGameArea;
            Events.GamePlayEvents.OnAnyGridCellMouseOneClicked -= OnAnyGridCellMouseOneClicked;
        }
        #endregion

        #region Event Listeners
        private void OnAnyGridCellMouseOneClicked(GridCell gridCell)
        {
            UIBuildingInformationArea.Hide(hitEvent: false,animated:false);
        }
        private void OnGameWindowBuildingClick(BuildingSO buildingSO)
        {
            UIBuildingInformationArea.Show(hitEvent:false);
            UIBuildingInformationArea.ApplyData(new BuildingInformationData() {gameUnitSO=buildingSO, gameUnit=null });
        }
        private void OnAnyUnitSelectedInGameArea(IGameUnit gameUnit)
        {
            UIBuildingInformationArea.Show(hitEvent: false);
            UIBuildingInformationArea.ApplyData(new BuildingInformationData() { gameUnitSO = gameUnit.gameUnitSO, gameUnit = gameUnit });
        }
        #endregion


    }
}