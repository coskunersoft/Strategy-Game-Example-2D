using System.Collections;
using System.Collections.Generic;
using AOP.EventFactory;
using AOP.GamePlay.Units;
using AOP.GridSystem;
using AOP.GamePlay.FX;
using AOP.DataCenter;
using AOP.ObjectPooling;
using UnityEngine;
using AOP.Extensions;

namespace AOP.GamePlay.Squance
{
    public class MilitaryUnitControlJob : IGameSquanceJob
    {
        private readonly GameGrid gameGrid;
        private IGameMilitaryUnit controllingMilitaryUnit;
        private GameDataSO gameDataSO;

        public MilitaryUnitControlJob(GameGrid gameGrid)
        {
            this.gameGrid = gameGrid;
        }

        #region Job Functions
        public override void Started()
        {
            gameDataSO = ObjectCamp.PullScriptable<GameDataSO>();

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
            ShowMoveParticle(((GridWorldCell)targetCell).transform.position);
        }
        private void AttackUnit(IGameUnit targetUnit)
        {
            controllingMilitaryUnit.StartAttack(targetUnit);
            ShowAttackParticle(targetUnit.transform.position);
        }

        private async void ShowAttackParticle(Vector2 position)
        {
             var task=  ObjectCamp.PullObject<OneShotParticle>(gameDataSO.ClickAttackParticle.ParticleName);
            await task;
            task.Result.transform.TranslateTransformWithCoverZAxis(position);
            task.Result.Play();

        }
        private async void ShowMoveParticle(Vector2 position)
        {
            var task = ObjectCamp.PullObject<OneShotParticle>(gameDataSO.ClickMoveParticle.ParticleName);
            await task;
            task.Result.transform.TranslateTransformWithCoverZAxis(position);
            task.Result.Play();

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