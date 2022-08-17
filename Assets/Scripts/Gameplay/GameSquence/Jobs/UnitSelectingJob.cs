using System.Collections;
using System.Collections.Generic;
using AOP.EventFactory;
using AOP.GamePlay.ChangeableSystems;
using AOP.GamePlay.Units;
using AOP.GridSystem;
using UnityEngine;

namespace AOP.GamePlay.Squance
{
    public class UnitSelectingJob : IGameSquanceJob
    {
        public IGameUnit selectedUnit;

        public UnitSelectingJob()
        {

        }

        public override void Started()
        {
            Events.GamePlayEvents.OnAnyGridCellMouseOneClicked += OnAnyGridCellClicked;
            Events.GamePlayEvents.OnAnyUnitSelectedInGameArea += OnAnyUnitSelectedInGameArea;

        }

        public override void Canceled()
        {

        }

        public override void Completed()
        {
            Events.GamePlayEvents.OnAnyGridCellMouseOneClicked -= OnAnyGridCellClicked;
            Events.GamePlayEvents.OnAnyUnitSelectedInGameArea -= OnAnyUnitSelectedInGameArea;

        }

        public override bool CompleteRule()
        {
            return false;
        }

        public override void Continue()
        {

        }

      
        #region EventListener

        private void OnAnyGridCellClicked(GridCell cell)
        {
            if (selectedUnit)
            {
                selectedUnit.HealthSystem.onAmountEmpty -= OnSelectedUnitDead;
                selectedUnit.DeSelect();
            }
            selectedUnit = null;
        }
        private void OnAnyUnitSelectedInGameArea(IGameUnit gameUnit)
        {
            if (selectedUnit) selectedUnit.DeSelect();
            selectedUnit = gameUnit;
            selectedUnit.HealthSystem.onAmountEmpty += OnSelectedUnitDead;
            selectedUnit.Select();
        }

        private void OnSelectedUnitDead(IDamage damage)
        {
            if (selectedUnit)
            {
                selectedUnit.DeSelect();
                selectedUnit = null;
            }
        }

        #endregion
    }
}
