using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.ObjectPooling;
using AOP.DataCenter;

namespace AOP.GridSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GridWorldCell : MonoBehaviour, IObjectCampMember
    {
        private SpriteRenderer spriteRenderer;
        private CellGroundType cellGroundType;

        private void Awake()
        {
            TryGetComponent(out spriteRenderer);
        }

        public void Apply(CellGroundType cellGroundType,Vector3 cellPosition)
        {
            this.cellGroundType = cellGroundType;
            transform.position = cellPosition;

            var configurationSO = ObjectCamp.PullScriptable<GridConfigurationSO>();
            var findedSpriteMap = configurationSO.cellGroundTypeSpriteMaps.Find(x => x.CellGroundType == cellGroundType);
            spriteRenderer.sprite = findedSpriteMap.sprite;
        }
    }
}
