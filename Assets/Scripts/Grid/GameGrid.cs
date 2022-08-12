using System.Collections;
using System.Collections.Generic;
using AOP.DataCenter;
using AOP.ObjectPooling;
using UnityEngine;

namespace AOP.GridSystem
{
    public class GameGrid 
    {
        private readonly GridCell[,] cells;

        public GameGrid(GridConfigurationSO configurationSO, int GridSize,Vector2 gridCenterPoint)
        {
            Vector2 pos = gridCenterPoint;
            cells = new GridCell[GridSize, GridSize];
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    cells[i, j] = new GridCell();
                    cells[i, j].Apply(CellGroundType.Grass, pos);
                    pos.x += configurationSO.GridCellDistance;
                }
                pos.x = gridCenterPoint.x;
                pos.y += configurationSO.GridCellDistance;
            }
        }
    }
}
