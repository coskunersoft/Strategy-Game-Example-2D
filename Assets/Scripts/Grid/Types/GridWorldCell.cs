using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.ObjectPooling;
using AOP.DataCenter;
using AOP.EventFactory;
using AOP.GamePlay.Units;

namespace AOP.GridSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GridWorldCell : MonoBehaviour, IObjectCampMember
    {
        private SpriteRenderer spriteRenderer;
        private CellGroundType cellGroundType;
        private GridCell gridCell;

        private void Awake()
        {
            TryGetComponent(out spriteRenderer);
        }

        public void Apply(GridCell gridCell, CellGroundType cellGroundType,Vector3 cellPosition)
        {
            this.cellGroundType = cellGroundType;
            this.gridCell = gridCell;
            transform.position = cellPosition;

            var configurationSO = ObjectCamp.PullScriptable<GridConfigurationSO>();
            var findedSpriteMap = configurationSO.cellGroundTypeSpriteMaps.Find(x => x.CellGroundType == cellGroundType);
            spriteRenderer.sprite = findedSpriteMap.sprite;
        }

        private void OnMouseUpAsButton()
        {
            if ((IGameUnit)gridCell)
                Events.GamePlayEvents.OnAnyUnitSelectedInGameArea?.Invoke(gridCell);
            else
                Events.GamePlayEvents.OnAnyGridCellClicked?.Invoke(gridCell);
            
        }
        private void OnMouseEnter()
        {
            Events.GamePlayEvents.OnMouseEnterAnyGridCell?.Invoke(gridCell);
        }
        private void OnMouseExit()
        {
            Events.GamePlayEvents.OnMouseExitAnyGridCell?.Invoke(gridCell);
        }

        public static implicit operator GridCell(GridWorldCell grid) => grid.gridCell;


    }
}
