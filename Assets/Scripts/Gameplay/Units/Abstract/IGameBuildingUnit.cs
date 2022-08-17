using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.GridSystem;
using AOP.DataCenter;
using AOP.GamePlay.ChangeableSystems;
using AOP.ObjectPooling;
using AOP.GamePlay.FX;
using AOP.Extensions;

namespace AOP.GamePlay.Units
{
    public abstract class IGameBuildingUnit : IGameUnit
    {
        [SerializeField] protected SpriteRenderer placingEffect;
        [HideInInspector] public BuildingSO buildingSO=>gameUnitSO as BuildingSO;

        public void HidePlacingEffect() => placingEffect.enabled = false;
        public void PlacingEffectColor(Color32 color)
        {
            placingEffect.enabled = true;
            placingEffect.color = color;
        }
        public override void Place(List<GridCell> gridCells)
        {
            base.Place(gridCells);
            HidePlacingEffect();
        }

        private async void ShowExplosionParticle()
        {
            var particleCreateTask = ObjectCamp.PullObject<OneShotParticle>(variation: buildingSO.ExplosionParticle.ParticleName);
            await particleCreateTask;
            particleCreateTask.Result.transform.TranslateTransformWithCoverZAxis(transform.position);
            particleCreateTask.Result.Play();

        }

        protected override void OnDead(IDamage damage)
        {
            ShowExplosionParticle();
            base.OnDead(damage);
        }
    }
}

