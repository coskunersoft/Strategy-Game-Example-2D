using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.GridSystem
{
    public class GameGrid 
    {
        private readonly GridCell[,] cells;

        public GameGrid(int GridSize,Vector2 startPosition)
        {
            Vector2 pos = startPosition;
            cells = new GridCell[GridSize, GridSize];
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    cells[i, j] = new GridCell();
                    cells[i, j].Apply(CellGroundType.Grass, pos);
                    pos.x += 1;
                }
                pos.x = startPosition.x;
                pos.y += 1;
            }
        }
    }
}
