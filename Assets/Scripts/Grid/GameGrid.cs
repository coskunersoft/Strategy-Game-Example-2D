using System.Collections;
using System.Collections.Generic;
using AOP.DataCenter;
using AOP.ObjectPooling;
using UnityEngine;
using System;
using AOP.Pathfinding;
using AOP.Pathfinding.Options;
using AOP.Pathfinding.Heuristics;

namespace AOP.GridSystem
{
    public class GameGrid
    {
        private readonly int gridSize = 0;
        private readonly GridCell[,] cells;
        public int[,] GridTiles;
        private PathFinder pathfinder;
        PathFinderOptions pathfinderOptions = new PathFinderOptions
        {
            PunishChangeDirection = true,
            UseDiagonals = true,
             HeuristicFormula= HeuristicFormula.Euclidean,
            
        };

        public GameGrid(GridConfigurationSO configurationSO, int gridSize, Vector2 gridCenterPoint)
        {
            this.gridSize = gridSize;
            Vector2 pos = gridCenterPoint;
            Coordinate cellCoordinate = new Coordinate(0, 0);
            cells = new GridCell[gridSize, gridSize];
            GridTiles = new int[gridSize, gridSize];
            for (int j = 0; j < gridSize; j++)
            {
                for (int i = 0; i < gridSize; i++)
                {
                    cellCoordinate = new Coordinate(i, j);
                    GridCell gridCell = new GridCell(CellUpdated);
                    gridCell.Apply(CellGroundType.Grass, pos, cellCoordinate);
                    cells[i, j] = gridCell;
                    GridTiles[i, j] = 1;
                    pos.x += configurationSO.GridCellDistance;
                }
                pos.x = gridCenterPoint.x;
                pos.y += configurationSO.GridCellDistance;
            }

            
        }


        
        public GridCell GetCellsNeighbor(GridCell cellMain, Direction dir)
        {
            if (cellMain == null) return null;
            Coordinate NextCoordinate = cellMain.cellCoordinate;
            NextCoordinate.Translate(dir);
            if (!GridContains(NextCoordinate) || NextCoordinate == cellMain.cellCoordinate) return default;
            return this[NextCoordinate];
        }
        public List<GridCell> GetCellsNeighbors(GridCell cellMain,bool onlyFreeCell=false, params Direction[] directions)
        {
            List<GridCell> result = new List<GridCell>();
            foreach (var item in directions)
            {
                result.Add(GetCellsNeighbor(cellMain, item));
            }
            result.RemoveAll(x => x == null);
            if (onlyFreeCell)
                result.RemoveAll(x => !x.CanPlaceUnit());
            return result;
        }
        public List<GridCell> GetCellsNeighborsLayer(GridCell gridCell, int layers, bool onlyFreeCell=false, bool includeMain=false)
        {
            List<GridCell> result = new List<GridCell>();
            int maxX = Math.Clamp(gridCell.cellCoordinate.x + layers, 0, gridSize-1);
            int minX = Math.Clamp(gridCell.cellCoordinate.x - layers, 0, gridSize-1);
            int maxY = Math.Clamp(gridCell.cellCoordinate.y + layers, 0, gridSize-1);
            int minY = Math.Clamp(gridCell.cellCoordinate.y - layers, 0, gridSize-1);

            for (int i = minY; i <= maxY; i++)
                for (int j = minX; j <= maxX; j++)
                    result.Add(cells[j, i]);

            if (!includeMain)
                result.Remove(gridCell);

            if (onlyFreeCell)
                result.RemoveAll(x => !x.CanPlaceUnit());

            return result;
            
        }
        public int DistanceTwoCell(GridCell a,GridCell b)
        {
            return DistanceTwoCoordinate(a.cellCoordinate, b.cellCoordinate);
        }
        public int DistanceTwoCoordinate(Coordinate a, Coordinate b)
        {
            return (int)Vector2.Distance(new Vector2(a.x, a.y), new Vector2(b.x, b.y));
        }
        public bool GridContains(Coordinate coordinate)
        {
            if (coordinate.x < 0 || coordinate.y < 0) return false;
            return gridSize > coordinate.y && gridSize > coordinate.x;
        }
        public void CellUpdated(GridCell cell)
        {
            GridTiles[cell.cellCoordinate.x,cell.cellCoordinate.y] = cell.CanPlaceUnit() ? 1 : 0;
            pathfinder = new PathFinder(new WorldGrid(GridTiles), pathfinderOptions);

        }
        public Coordinate[] GetMovementPath(Coordinate a,Coordinate b)
        {
           return pathfinder.FindPath(a, b);
        }


        public GridCell this[Coordinate coordinate] => cells[coordinate.x,coordinate.y];
    }
}
