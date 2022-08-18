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
        public Coordinate cellCoordinate;
        public Vector2 WorldPosition => gridWorldCell ? (Vector2)gridWorldCell.transform.position :Vector2.zero;
        private IGameUnit placedUnit;
        private System.Action<GridCell> OnCellUpdated;

        public GridCell(System.Action<GridCell> OnCellUpdated)
        {
            this.OnCellUpdated = OnCellUpdated;
        }
        public void Apply(CellGroundType cellGroundType,Vector2 cellWorldPosition,Coordinate cellCoordinate)
        {
            this.cellGroundType = cellGroundType;
            this.cellCoordinate = cellCoordinate;
            Visualize(cellWorldPosition);
        }
        private async void Visualize(Vector2 cellWorldPosition)
        {
            if (!gridWorldCell)
            {
                var task = ObjectCamp.PullObject<GridWorldCell>(PoolStaticVariations.VARIATION1);
                await task;
                gridWorldCell = task.Result;
            }
            gridWorldCell.Apply(this,cellGroundType, cellWorldPosition);
        }
        public bool CanPlaceUnit()
        {
            if (placedUnit!=null) return false;
            return true;
        }
        public void PlaceUnit(IGameUnit gameUnit)
        {
            placedUnit = gameUnit;
            OnCellUpdated?.Invoke(this);
        }
        public void UnPlaceUnit()
        {
            placedUnit = null;
            OnCellUpdated?.Invoke(this);
        }

        public static implicit operator GridWorldCell(GridCell grid)=>grid.gridWorldCell;
        public static implicit operator IGameUnit(GridCell grid) => grid.placedUnit;
    }
}
