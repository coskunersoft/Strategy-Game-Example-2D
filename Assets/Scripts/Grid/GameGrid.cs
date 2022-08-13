using System.Collections;
using System.Collections.Generic;
using AOP.DataCenter;
using AOP.ObjectPooling;
using UnityEngine;
using System;

namespace AOP.GridSystem
{
    public class GameGrid 
    {
        private readonly GridCell[,] cells;
        public GameGrid(GridConfigurationSO configurationSO, int gridSize, Vector2 gridCenterPoint)
        {
            Vector2 pos = gridCenterPoint;
            Vector2 cellCoordinate = Vector2.zero;
            cells = new GridCell[gridSize, gridSize];
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    (cellCoordinate.x, cellCoordinate.y) = (j, i);
                    cells[j, i] = new GridCell();
                    cells[j, i].Apply(CellGroundType.Grass, pos,cellCoordinate);
                    pos.x += configurationSO.GridCellDistance;
                }
                pos.x = gridCenterPoint.x;
                pos.y += configurationSO.GridCellDistance;
            }
        }

        public GridCell GetCellsNeighbor(GridCell cellMain, Direction dir)
        {
            if (cellMain == null) return null;
            Vector2 NextCoordinate = cellMain.cellCoordinate;
            switch (dir)
            {
                case Direction.Right:
                    NextCoordinate.x += 1;
                    break;
                case Direction.Left:
                    NextCoordinate.x -= 1;
                    break;
                case Direction.Up:
                    NextCoordinate.y += 1;
                    break;
                case Direction.Down:
                    NextCoordinate.y -= 1;
                    break;
                case Direction.RightDown:
                    NextCoordinate.x += 1;
                    NextCoordinate.y -= 1;
                    break;
                case Direction.LeftDown:
                    NextCoordinate.x -= 1;
                    NextCoordinate.y -= 1;
                    break;
                case Direction.RightUp:
                    NextCoordinate.x += 1;
                    NextCoordinate.y += 1;
                    break;
                case Direction.LeftUp:
                    NextCoordinate.x -= 1;
                    NextCoordinate.y += 1;
                    break;
            }
            
            if (!GridContains(NextCoordinate)||NextCoordinate == cellMain.cellCoordinate) return default;
            return this[NextCoordinate];
        }
        public List<GridCell> GetCellsNeighbors(GridCell cellMain,params Direction[] directions)
        {
            List<GridCell> result = new List<GridCell>();
            foreach (var item in directions)
            {
                result.Add(GetCellsNeighbor(cellMain, item));
            }
            return result;
        }

        public bool GridContains(Vector2 coordinate)
        {
            int i = (int)coordinate.x;
            int j = (int)coordinate.y;
            if (i < 0 || j < 0) return false;
            return cells.GetLength(0) > j && cells.GetLength(1) > i;
        }

        public GridCell this[Vector2 coordinate]=>cells[(int)coordinate.x,(int)coordinate.y];
    }
}
