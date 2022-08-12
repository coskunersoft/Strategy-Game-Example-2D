using AOP.ObjectPooling;
using AOP.GridSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AOP.DataCenter
{
    [CreateAssetMenu(menuName = "AOP/Data/InGamePrefabSO", fileName = "AOP-Data-" + nameof(InGamePrefabsSO))]
    public class InGamePrefabsSO : IPrefabsContainerSO
    {
        public AssetReference GridWorldCellPrefab;

        public override void RegisterPrefabs()
        {
            ObjectCamp.RegisterPrefab(new TypePrefabRegisterMap(typeof(GridWorldCell), GridWorldCellPrefab, PoolStaticVariations.VARIATION1));
        }
    }
}

