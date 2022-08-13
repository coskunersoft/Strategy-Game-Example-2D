using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.GridSystem;

namespace AOP.GamePlay.Units
{
    public abstract class IGameBuildingUnit : IGameUnit
    {
        [SerializeField] protected SpriteRenderer placingEffect;
        protected List<GridCell> placedGridCells;

        public void HidePlacingEffect() => placingEffect.enabled = false;
        public void PlacingEffectColor(Color32 color)
        {
            placingEffect.enabled = true;
            placingEffect.color = color;
        }

        public virtual void Place(List<GridCell> gridCells)
        {
            HidePlacingEffect();
            placedGridCells = gridCells;
        }

    }
}

