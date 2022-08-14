using System.Collections;
using System.Collections.Generic;
using AOP.DataCenter;
using AOP.EventFactory;
using AOP.GamePlay.Units;
using AOP.GridSystem;
using AOP.ObjectPooling;
using UnityEngine;
using System.Linq;
using AOP.Extensions;

namespace AOP.GamePlay.Squance
{
    public class BuildPlaceJob : IGameSquanceJob
    {
        private readonly BuildingSO buildingSO;
        private GameDataSO gameDataSO;
        private readonly GameGrid gameGrid;
        private IGameBuildingUnit gameBuildingUnit;
        private Vector3 targetPosition;
      
        private GridWorldCell focusedCell;
        private List<GridCell> lastFocusedCells;

        public BuildPlaceJob(GameGrid gameGrid, BuildingSO buildingSO) : base()
        {
            this.buildingSO = buildingSO;
            this.gameGrid = gameGrid;
        }

        #region Job Functions
        public override async void Started()
        {
            gameDataSO = ObjectCamp.PullScriptable<GameDataSO>();
            var task = ObjectCamp.PullObject<IGameUnit>(buildingSO.UnitName);
            await task;
            gameBuildingUnit = task.Result as IGameBuildingUnit;
            gameBuildingUnit.buildingSO = buildingSO;
            targetPosition = gameBuildingUnit.transform.position;
            gameBuildingUnit.PlacingEffectColor(gameDataSO.WrongUnitPlaceColor);


            Events.GamePlayEvents.OnMouseEnterAnyGridCell += OnMouseEnterAnyGridCell;
            Events.GamePlayEvents.OnMouseExitAnyGridCell += OnMouseExitAnyGridCell;
        }

        public override void Canceled()
        {

        }

        public override bool CompleteRule()
        {
            return Input.GetMouseButtonUp(0);
        }

        public override void Continue()
        {
            FollowPointer();
        }

        public override void Completed()
        {
            if (lastFocusedCells==null) ObjectCamp.PushObject(gameBuildingUnit,buildingSO.UnitName);
            else
            {
                gameBuildingUnit.Place(lastFocusedCells);
                Events.GamePlayEvents.OnAnyBuildigPlaced?.Invoke(gameBuildingUnit);
                lastFocusedCells.ForEach(x => x.PlaceUnit(gameBuildingUnit));
            }
            
            Events.GamePlayEvents.OnMouseEnterAnyGridCell -= OnMouseEnterAnyGridCell;
            Events.GamePlayEvents.OnMouseExitAnyGridCell -= OnMouseExitAnyGridCell;
        }
        #endregion

        #region EventListeners
        private void OnMouseExitAnyGridCell(GridCell gridCell)
        {
            focusedCell = null;
            lastFocusedCells = null;
            gameBuildingUnit.PlacingEffectColor(gameDataSO.WrongUnitPlaceColor);
        }
        private void OnMouseEnterAnyGridCell(GridCell gridCell)
        {
            focusedCell = gridCell;
            lastFocusedCells = FindGridCellsWithOriantation();
            if (lastFocusedCells == null)
                gameBuildingUnit.PlacingEffectColor(gameDataSO.WrongUnitPlaceColor);
            else
                gameBuildingUnit.PlacingEffectColor(gameDataSO.RightUnitPlaceColor);
        }
        #endregion

        private void FollowPointer()
        {
            if (!gameBuildingUnit) return;
            if (focusedCell != null&&lastFocusedCells!=null)
            {
                var center = lastFocusedCells.ConvertAll(x => (Vector2)(((GridWorldCell)x).transform.position)).CenterOfVectors();
                targetPosition = center;
                targetPosition.z = gameBuildingUnit.transform.position.z;
                gameBuildingUnit.transform.position = targetPosition;
            }
            else
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.z = gameBuildingUnit.transform.position.z;
                gameBuildingUnit.transform.position = targetPosition;
            }
        }
        private List<GridCell> FindGridCellsWithOriantation()
        {
            List<GridCell> result = new List<GridCell>();
            GridCell currentCell = focusedCell;
            result.Add(currentCell);
            foreach (var directionGroup in buildingSO.buildingOrtiantationProfileSO.OriantationGroups)
            {
                currentCell = focusedCell;
                foreach (var direction  in directionGroup.directions)
                {
                    if (direction is Direction.None) continue;
                    currentCell = gameGrid.GetCellsNeighbor(currentCell, direction);
                    result.Add(currentCell);
                }
            }
            if (result.Any(x => x == null)) return null;
            if (result.Any(x => !x.CanPlaceUnit())) return null;
            return result;
        }
    }

}

