using System.Collections;
using System.Collections.Generic;
using AOP.DataCenter;
using AOP.ObjectPooling;
using UnityEngine;
using DG.Tweening;
using AOP.EventFactory;

namespace AOP.GamePlay.Units
{
    public class IGameBarrackBuildingUnit : IGameBuildingUnit
    {
        [HideInInspector] public BarrackBuildingSO barrackBuildingSO => buildingSO as BarrackBuildingSO;
        public MilitaryUnitSO currentProductionUnit { get; private set; }
        public float ProductionProgressTime { get; private set; }
        private Tween ProductionTween;
        private float productionTime;

        public virtual void StartProduction(MilitaryUnitSO militaryUnitSO)
        {
            if (currentProductionUnit) return;
            currentProductionUnit = militaryUnitSO;
            productionTime = 0;
            Events.GamePlayEvents.OnAnyBarrackBuildingStartedProducting?.Invoke(this, currentProductionUnit);
            ProductionTween = DOTween.To(() => productionTime, x => productionTime = x, militaryUnitSO.ProductionTime, militaryUnitSO.ProductionTime)
                .SetEase(Ease.Linear).OnUpdate(ProductionProgress).OnComplete(CreateProduction);
        }
        public void CancelProduction()
        {
            ProductionTween.Kill();
            Events.GamePlayEvents.OnAnyBarrackBuildingCancelProducting?.Invoke(this, currentProductionUnit);

        }
        private void ProductionProgress()
        {
            ProductionProgressTime = productionTime/currentProductionUnit.ProductionTime;
            Events.GamePlayEvents.OnAnyBarrackBuildingProductingProgress?.Invoke(this, currentProductionUnit);
        }
        private async void CreateProduction()
        {
            var createProductionTask = ObjectCamp.PullObject<IGameUnit>(variation: currentProductionUnit.UnitName);
            await createProductionTask;
            createProductionTask.Result.Initialize(currentProductionUnit);
            currentProductionUnit = null;
            Events.GamePlayEvents.OnAnyBarrackBuildingFinishProducting?.Invoke(this, createProductionTask.Result as IGameMilitaryUnit);
        }
    }
}