using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.ObjectPooling;

namespace AOP.GridSystem
{
    public class GridCell
    {
        public CellGroundType cellGroundType { get;private set; }
        private GridWorldCell gridWorldCell;
        private Vector2 cellPosition;
        
        public void Apply(CellGroundType cellGroundType,Vector2 cellPosition)
        {
            this.cellGroundType = cellGroundType;
            this.cellPosition = cellPosition;
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
            gridWorldCell.Apply(cellGroundType, cellPosition);
        }
        public static implicit operator GridWorldCell(GridCell grid)=>grid.gridWorldCell;
    }
}
