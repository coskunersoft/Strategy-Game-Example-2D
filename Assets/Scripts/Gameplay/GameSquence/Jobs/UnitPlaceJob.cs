using System.Collections;
using System.Collections.Generic;
using AOP.GamePlay.Units;
using AOP.GridSystem;
using UnityEngine;
using AOP.Extensions;

namespace AOP.GamePlay.Squance
{
    public class UnitPlaceJob : IGameSquanceJob
    {
        private readonly IGameMilitaryUnit gameMilitaryUnit;
        private readonly GameGrid gameGrid;
        private readonly IGameBarrackBuildingUnit gameBarrackBuildingUnit;
        private bool placed;
        private float tryTime = 0; 

        public UnitPlaceJob(GameGrid gameGrid, IGameMilitaryUnit gameMilitaryUnit, IGameBarrackBuildingUnit gameBarrackBuildingUnit)
        {
            this.gameMilitaryUnit = gameMilitaryUnit;
            this.gameGrid = gameGrid;
            this.gameBarrackBuildingUnit = gameBarrackBuildingUnit;
        }

        public override void Canceled()
        {
           
        }

        public override void Completed()
        {

        }

        public override bool CompleteRule()
        {
            return placed;
        }

        public override void Continue()
        {
            if (Time.time<tryTime) return;
            
            tryTime = Time.time + 0.25f;
            TryPlace();
        }

        public override void Started()
        {
            gameMilitaryUnit.gameObject.SetActive(false);
        }

        private void TryPlace()
        {
            try
            {
                List<GridCell> aveliableCells = new List<GridCell>();
                foreach (var item in gameBarrackBuildingUnit.PlacedGridCells)
                {
                    GridCell finded = item;
                    foreach (var directionGroup in gameBarrackBuildingUnit.barrackBuildingSO.UnitSpawnPointProfile.OriantationGroups)
                    {
                        finded = item;
                        foreach (var direction in directionGroup.directions)
                        {
                            finded = gameGrid.GetCellsNeighbor(finded, direction);
                        }
                        aveliableCells.Add(finded);
                    }
                }
                aveliableCells = aveliableCells.FindAll(x => x != null);
                aveliableCells = aveliableCells.FindAll(x => x.CanPlaceUnit());
                if (aveliableCells.Count > 0)
                {
                    Vector2 centerofCoordinate = gameBarrackBuildingUnit.PlacedGridCells.ConvertAll(x => (Vector2)x.cellCoordinate).CenterOfVectors();
                    aveliableCells.Sort((x, y) => Vector2.Distance(centerofCoordinate, x.cellCoordinate).CompareTo(Vector2.Distance(centerofCoordinate, y.cellCoordinate)));
                    if (aveliableCells.Count > 1)
                        aveliableCells.RemoveRange(1, aveliableCells.Count - 1);
                    gameMilitaryUnit.Place(aveliableCells);
                    gameMilitaryUnit.transform.TranslateTransformWithCoverZAxis(aveliableCells[0].WorldPosition);
                    aveliableCells[0].PlaceUnit(gameMilitaryUnit);
                    gameMilitaryUnit.gameObject.SetActive(true);
                    placed = true;
                }
            }
            catch { }
        }
    }
}
