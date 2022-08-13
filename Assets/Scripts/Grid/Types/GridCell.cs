using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.ObjectPooling;
using AOP.GamePlay.Units;

namespace AOP.GridSystem
{
    public class GridCell
    {
        public CellGroundType cellGroundType { get;private set; }
        private GridWorldCell gridWorldCell;
        public Vector2 cellCoordinate;
        private Vector2 cellPosition;
        private IGameUnit placedUnit;
        
        public void Apply(CellGroundType cellGroundType,Vector2 cellPosition,Vector2 cellCoordinate)
        {
            this.cellGroundType = cellGroundType;
            this.cellPosition = cellPosition;
            this.cellCoordinate = cellCoordinate;
            Visualize();
        }
        private async void Visualize()
        {
            if (!gridWorldCell)
            {
                var task = ObjectCamp.PullObject<GridWorldCell>(PoolStaticVariations.VARIATION1);
                await task;
                gridWorldCell = task.Result;
            }
            gridWorldCell.Apply(this,cellGroundType, cellPosition);
        }
        public bool CanPlaceUnit()
        {
            if (placedUnit != null) return false;
            return true;
        }
        public void PlaceUnit(IGameUnit gameUnit)
        {
            placedUnit = gameUnit;
        }

        public static implicit operator GridWorldCell(GridCell grid)=>grid.gridWorldCell;
        public static implicit operator IGameUnit(GridCell grid) => grid.placedUnit;
    }
}
