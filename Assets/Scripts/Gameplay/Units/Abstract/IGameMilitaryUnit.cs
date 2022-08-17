using System.Collections;
using System.Collections.Generic;
using AOP.DataCenter;
using AOP.GamePlay.Navigation;
using AOP.GamePlay.ChangeableSystems;
using AOP.GridSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;
using AOP.Extensions;
using AOP.ObjectPooling;
using AOP.GamePlay.FX;

namespace AOP.GamePlay.Units
{
    [RequireComponent(typeof(NavigationAgent))]
    public abstract class IGameMilitaryUnit : IGameUnit
    {
        [HideInInspector] public MilitaryUnitSO militaryUnitSO => gameUnitSO as MilitaryUnitSO;
        [ReadOnly] public NavigationAgent NavigationAgent;
        protected abstract IDamage AttackDamage { get; }
        public GameGrid gameGrid;
        protected IGameUnit currentTarget;
        private Coordinate attackCoordinate;

        private WaitForSeconds waitForAttackRate;
        private Coroutine currentStrikeCoroutine;

        #region Mono Functions
        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out NavigationAgent);
        }
        #endregion

        #region GameMilitaryUnit Bussiness
        public void InitilizeMilitiayUnit(GameGrid gameGrid)
        {
            this.gameGrid = gameGrid;
            NavigationAgent.InitializeGrid(gameGrid);
            AttackDamage.amount = militaryUnitSO.StrikePower;
            waitForAttackRate = new WaitForSeconds(militaryUnitSO.AttackRate);
        }
        public void StartAttack(IGameUnit target)
        {
            Debug.Log(transform.name + " Start Attack " + target.transform.name);
            currentTarget = target;
            CancelAttack();
            currentStrikeCoroutine=StartCoroutine(AttackCoroutine());
        }
        public virtual void AttackToEnemy()
        {
            if (!currentTarget) return;
            ShowHitParticle(currentTarget.transform.position);
            LookAtTargetPoint2D(currentTarget.transform.position);
            currentTarget.HealthSystem.TakeDamage(AttackDamage);
        }
        private async void ShowHitParticle(Vector2 Position)
        {
            var particleCreateTask = ObjectCamp.PullObject<OneShotParticle>(variation: militaryUnitSO.HitToEnemyParticleSO.ParticleName);
            await particleCreateTask;
            particleCreateTask.Result.transform.TranslateTransformWithCoverZAxis(Position);
            particleCreateTask.Result.Play();

        }
        public void CancelAttack()
        {
            if (currentStrikeCoroutine != null) StopCoroutine(currentStrikeCoroutine);
        }
        private IEnumerator AttackCoroutine()
        {
            ReControlArea:

            if (!IsEnemyInAttackRange())
            {
                attackCoordinate = FindAttackCoordinate();
                NavigationAgent.SetDestination(attackCoordinate);
                int waitingStep = 0;
                while (true)
                {
                    waitingStep++;
                    yield return new WaitForSeconds(.5f);
                    if (IsAttackPointReached()||waitingStep>=4) break;
                }
                if (!IsEnemyInAttackRange())
                {
                    goto ReControlArea;
                }
            }


            currentTarget.HealthSystem.onAmountEmpty += OnTargetDead;
           

            bool targetLost = false;
            while (currentTarget!=null&&currentTarget.HealthSystem.Amount>0)
            {
                yield return waitForAttackRate;
                if (IsEnemyInAttackRange())
                {
                    AttackToEnemy();
                }
                else
                {
                  
                    targetLost = true;
                    break;
                }
               
            }
            if (targetLost)
            {
                if (currentTarget != null)
                {
                    currentTarget.HealthSystem.onAmountEmpty -= OnTargetDead;
                    StartAttack(currentTarget);
                }
                yield break;
            }
            else
            {
                if (currentTarget != null)
                {
                    currentTarget.HealthSystem.onAmountEmpty -= OnTargetDead;
                    currentTarget = null;
                }
            }
            
        }
        private Coordinate FindAttackCoordinate()
        {
            GridCell owngridCell = placedGridCells[0];
            List<GridCell> targetCells = new List<GridCell>();
            foreach (var targetPlacedCell in currentTarget.PlacedGridCells)
            {
                targetCells.AddRange(gameGrid.GetCellsNeighborsLayer(targetPlacedCell, militaryUnitSO.AttackDistance, onlyFreeCell: true, includeMain: false));
            }
            targetCells.Sort((x, y) => gameGrid.DistanceTwoCell(x, owngridCell).CompareTo(gameGrid.DistanceTwoCell(y, owngridCell)));
            GridCell resultCell = targetCells.FirstOrDefault();
            return resultCell != null ? targetCells.FirstOrDefault().cellCoordinate : default;
           
        }
        private bool IsEnemyInAttackRange()
        {
            if (placedGridCells == null || currentTarget == null || currentTarget.PlacedGridCells == null) return false;
            return gameGrid.DistanceTwoCell(currentTarget.PlacedGridCells[0], placedGridCells[0]) <=militaryUnitSO.AttackDistance;
        }
        private bool IsAttackPointReached()
        {
            if (placedGridCells == null) return false;
            return placedGridCells[0].cellCoordinate == attackCoordinate;
        }
        public void LookAtTargetPoint2D(Vector2 position)
        {
            Vector3 scale = OriginalScale;
            if (transform.position.x > position.x)
            {
                scale.x *= -1;
            }
      
            transform.localScale = scale;
            /*
            Vector3 vectorToTarget = transform.NewPositionWithCoverZAxis(position) - transform.position;
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
            transform.rotation = targetRotation;
            */
        }
        #endregion

        #region Event Listeners
        private void OnTargetDead(IDamage damage)
        {
            if (currentTarget != null)
            {
                currentTarget.HealthSystem.onAmountEmpty -= OnTargetDead;
                currentTarget = null;
            }
        }
        #endregion
    }
}

