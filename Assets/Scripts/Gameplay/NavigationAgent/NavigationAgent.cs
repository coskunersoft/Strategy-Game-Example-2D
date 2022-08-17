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
        private GameGrid gameGrid;
        private IGameMilitaryUnit gameUnit;
        private Coordinate[] currentPath;
        public System.Action OnMovementStarted;
        public System.Action OnMovementCompleted;
        public System.Action OnMovementImposible;

        private Coroutine currentMovementCoroutine;

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
        }
        
       

        private void Move(Coordinate coordinate)
        {
            if (currentMovementCoroutine!=null) StopCoroutine(currentMovementCoroutine);
            currentMovementCoroutine = StartCoroutine(MoveCoroutine(coordinate));
        }

        private IEnumerator MoveCoroutine(Coordinate coordinate)
        {
            currentPath = gameGrid.GetMovementPath(gameUnit.PlacedGridCells[0].cellCoordinate,coordinate);
            if (currentPath.Length <= 1)
            {
                OnMovementImposible?.Invoke();
                Debug.Log("Imposible Movement "+coordinate+" --- "+ gameUnit.PlacedGridCells[0].cellCoordinate);
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
                    int step = 0;
                    for (step = 0; step < 11; step++)
                    {
                        yield return new WaitForSeconds(.1f);
                        if (!targetCell.CanPlaceUnit()) break;
                    }
                    if(step>=10)
                    {
                        Move(coordinate);
                        yield break;
                    }
                }
                gameUnit.PlacedGridCells.ForEach(x => x.UnPlaceUnit());
                gameUnit.Place(new List<GridCell>() { targetCell });
                targetCell.PlaceUnit(gameUnit);
                yield return gameUnit.transform.DOMove(gameUnit.transform.NewPositionWithCoverZAxis(targetCell.WorldPosition), 0.5f).SetEase(Ease.Linear).WaitForCompletion();
            }
            OnMovementCompleted?.Invoke();
        }

    }
}

