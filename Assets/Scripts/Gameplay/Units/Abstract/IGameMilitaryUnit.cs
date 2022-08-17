using System.Collections;
using System.Collections.Generic;
using AOP.DataCenter;
using AOP.GamePlay.Navigation;
using AOP.GamePlay.ChangeableSystems;
using AOP.GridSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;
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
            transform.DOPunchScale(Vector3.one, .5f);
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
            Debug.Log("Attack Canceled" + transform.name);
        }
        private IEnumerator AttackCoroutine()
        {
            ReControlArea:
            var movementData = NavigationAgent.Status();
            if (!IsEnemyInAttackRange())
            {
                attackCoordinate = FindAttackCoordinate();
                if (attackCoordinate == Coordinate.Empity) yield break;
                NavigationAgent.SetDestination(attackCoordinate);
                int waitingStep = 0;
                while (true)
                {
                    movementData = NavigationAgent.Status();
                    if (!movementData.IsRecalculating)
                    {
                        waitingStep++;
                    }
                    
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
            List<GridCell> targetCells = new List<GridCell>();
            foreach (var targetPlacedCell in currentTarget.PlacedGridCells)
            {
                targetCells.AddRange(gameGrid.GetCellsNeighborsLayer(targetPlacedCell, militaryUnitSO.AttackDistance, onlyFreeCell: false, includeMain: false));
            }
            var resultCell = targetCells.Find(x => ((IGameUnit)x) == this);
            targetCells = targetCells.FindAll(x => x != null);
            targetCells = targetCells.FindAll(x => x.CanPlaceUnit());
            if(resultCell==null) resultCell= targetCells.GetRandomElement();
            return resultCell != null ? resultCell.cellCoordinate : Coordinate.Empity;
        }
        private bool IsEnemyInAttackRange()
        {
            if (placedGridCells == null || currentTarget == null || currentTarget.PlacedGridCells == null) return false;
            var finded= currentTarget.PlacedGridCells.Find(x => gameGrid.DistanceTwoCell(x, placedGridCells[0] )<= militaryUnitSO.AttackDistance);
            return finded!=null;
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

