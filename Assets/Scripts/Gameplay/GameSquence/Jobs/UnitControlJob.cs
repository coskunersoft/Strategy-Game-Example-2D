using System.Collections;
using System.Collections.Generic;
using AOP.EventFactory;
using AOP.GamePlay.Units;
using AOP.GridSystem;
using UnityEngine;
using AOP.Extensions;
using AOP.FunctionFactory;
using DG.Tweening;

namespace AOP.GamePlay.Squance
{
    public class MilitaryUnitControlJob : IGameSquanceJob
    {
        private readonly GameGrid gameGrid;
        private IGameMilitaryUnit controllingMilitaryUnit;

        public MilitaryUnitControlJob(GameGrid gameGrid)
        {
            this.gameGrid = gameGrid;
        }

        #region Job Functions
        public override void Started()
        {
            Events.GamePlayEvents.OnAnyGridCellMouseOneClicked += OnAnyGridCellClicked;
            Events.GamePlayEvents.OnAnyGridCellMouseTwoClicked += OnAnyGridCellMouseTwoClicked;
            Events.GamePlayEvents.OnAnyUnitSelectedInGameArea += OnAnyUnitSelectedInGameArea;
        }
        public override void Canceled()
        {

        }
        public override void Completed()
        {
            Events.GamePlayEvents.OnAnyGridCellMouseOneClicked -= OnAnyGridCellClicked;
            Events.GamePlayEvents.OnAnyGridCellMouseTwoClicked -= OnAnyGridCellMouseTwoClicked;
            Events.GamePlayEvents.OnAnyUnitSelectedInGameArea -= OnAnyUnitSelectedInGameArea;
        }
        public override bool CompleteRule()
        {
            return false;
        }
        public override void Continue()
        {
            if (controllingMilitaryUnit)
            {
                if (controllingMilitaryUnit.HealthSystem.isEmpty)
                    controllingMilitaryUnit = null;
            }
        }
        #endregion

        private void MoveUnit(GridCell targetCell)
        {
            controllingMilitaryUnit.CancelAttack();
            controllingMilitaryUnit.NavigationAgent.SetDestination(targetCell.cellCoordinate);
        }
        private void AttackUnit(IGameUnit targetUnit)
        {
            controllingMilitaryUnit.StartAttack(targetUnit);
        }

        #region EventListener
        private void OnAnyGridCellClicked(GridCell cell)
        {
            controllingMilitaryUnit = null;
        }
        private void OnAnyGridCellMouseTwoClicked(GridCell cell)
        {
            if (!controllingMilitaryUnit) return;
            IGameUnit gridsUnit = cell;
            if (gridsUnit)
            {
                if (gridsUnit == controllingMilitaryUnit) return;
                AttackUnit(gridsUnit);
            }
            else
            {
                MoveUnit(cell);
            }
           
        }
        private void OnAnyUnitSelectedInGameArea(IGameUnit gameUnit)
        {
            controllingMilitaryUnit = null;
            if(gameUnit is IGameMilitaryUnit gameMilitaryUnit)
            {
                controllingMilitaryUnit = gameMilitaryUnit;
                controllingMilitaryUnit.InitilizeMilitiayUnit(gameGrid);
            }
        }

        #endregion
    }
}