using System.Collections;
using System.Collections.Generic;
using AOP.DataCenter;
using AOP.GamePlay.ChangeableSystems;
using AOP.GamePlay.ChangeableSystems.Health;
using AOP.GridSystem;
using AOP.ObjectPooling;
using Sirenix.OdinInspector;
using UnityEngine;


namespace AOP.GamePlay.Units
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(HealthSystem))]
    public abstract class IGameUnit :MonoBehaviour ,IObjectCampMember
    {
        [ReadOnly]public HealthSystem HealthSystem;
        [SerializeField]private SpriteRenderer MainSpriteRenderer;
        public IGameUnitSO gameUnitSO;
        protected List<GridCell> placedGridCells;
         public Vector3 OriginalScale { get; private set;}
        public List<GridCell> PlacedGridCells=>placedGridCells;

        protected virtual void Awake()
        {
            TryGetComponent(out HealthSystem);
            HealthSystem.onAmountEmpty += OnDead;
            OriginalScale = transform.localScale;
            
        }
        public virtual void Initialize(IGameUnitSO gameUnitSO)
        {
            this.gameUnitSO = gameUnitSO;
            HealthSystem.InitAmount(gameUnitSO.MaxHealth);
        }

        public virtual void Place(List<GridCell> gridCells)
        {
            placedGridCells = gridCells;
        }
        public virtual void Select()
        {
            var gameDataSO=ObjectCamp.PullScriptable<GameDataSO>();
            MainSpriteRenderer.material = gameDataSO.UnitSelectedMaterial;
           
        }
        public virtual void DeSelect()
        {
            var gameDataSO = ObjectCamp.PullScriptable<GameDataSO>();
            MainSpriteRenderer.material = gameDataSO.UnitNormalMaterial;
        }

        protected virtual void OnDead(IDamage damage)
        {
            placedGridCells.ForEach(x => x.UnPlaceUnit());
            placedGridCells = null;
            ObjectCamp.PushObject(this,_variation: gameUnitSO.UnitName);
        }

    }

}
