using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.GridSystem;
using AOP.GamePlay.Units;
using DG.Tweening;
using AOP.Extensions;
using AOP.Exceptions;

namespace AOP.GamePlay.Navigation
{
    [RequireComponent(typeof(IGameMilitaryUnit))]
    public class NavigationAgent : MonoBehaviour
    {
        public bool IsGridInitialized { get;private set; }
        private bool IsMoving;
        private bool IsRecalculating = false;
    
        private Coordinate[] currentPath;
        private Coordinate currentTargetCoordinate;
        private GameGrid gameGrid;
        private IGameMilitaryUnit gameUnit;
        public System.Action OnMovementStarted;
        public System.Action OnMovementCompleted;
        public System.Action OnMovementImposible;

        private Coroutine currentMovementCoroutine;
        private Tween currentMovementTween;

        private void Awake()
        {
            TryGetComponent(out gameUnit);
        }
        public void InitializeGrid(GameGrid gameGrid)
        {
            this.gameGrid = gameGrid;
            IsGridInitialized = true;
        }

        public void SetDestination(Coordinate coordinate)
        {
            if (!IsGridInitialized) throw new NavigationAgentNotInitializedException(this);
            if (gameGrid == null) return;
            Move(coordinate);

        }
        public void Stop()
        {
            if (currentMovementCoroutine != null) StopCoroutine(currentMovementCoroutine);
            IsMoving = false;
            currentPath = null;
            currentTargetCoordinate = default;
        }

        public (bool IsMoving ,bool IsRecalculating, Coordinate currentTargetCoordinate, Coordinate[] currentPath) Status()
        {
            return (IsMoving,IsRecalculating,currentTargetCoordinate,currentPath);
        }
       

        private void Move(Coordinate coordinate)
        {
            if (currentMovementCoroutine!=null) StopCoroutine(currentMovementCoroutine);
            currentMovementCoroutine = StartCoroutine(MoveCoroutine(coordinate));
        }

        private IEnumerator MoveCoroutine(Coordinate coordinate)
        {
            currentPath = gameGrid.GetMovementPath(gameUnit.PlacedGridCells[0].cellCoordinate,coordinate);
            IsMoving = true;
            currentTargetCoordinate = coordinate;
            if (currentPath.Length <= 0)
            {
                OnMovementImposible?.Invoke();
                Debug.Log("Imposible Movement "+coordinate+" --- "+ gameUnit.PlacedGridCells[0].cellCoordinate);
                Stop();
                yield break;
            }
            OnMovementStarted?.Invoke();
            foreach (var item in currentPath)
            {
                if (item == gameUnit.PlacedGridCells[0].cellCoordinate) continue;
                var targetCell = gameGrid[item];

                gameUnit.LookAtTargetPoint2D(targetCell.WorldPosition);
                if (!targetCell.CanPlaceUnit())
                {
                    IsRecalculating = true;
                    int step = 0;
                    for (step = 0; step < 11; step++)
                    {
                        yield return new WaitForSeconds(.1f);
                        if (targetCell.CanPlaceUnit()) break;
                    }
                    IsRecalculating = false;
                    if (step>=10)
                    {
                        Move(coordinate);
                        yield break;
                    }
                }
                gameUnit.PlacedGridCells.ForEach(x => x.UnPlaceUnit());
                gameUnit.Place(new List<GridCell>() { targetCell });
                targetCell.PlaceUnit(gameUnit);
                if (currentMovementTween != null) currentMovementTween.Kill();
                currentMovementTween = gameUnit.transform.DOMove(gameUnit.transform.NewPositionWithCoverZAxis(targetCell.WorldPosition), 0.5f).SetEase(Ease.Linear);
                yield return currentMovementTween.WaitForCompletion();
            }
            OnMovementCompleted?.Invoke();
            Stop();
        }

    }
}

