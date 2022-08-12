using AOP.ObjectPooling;
using AOP.GridSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using AOP.GamePlay;

namespace AOP.DataCenter
{
    [CreateAssetMenu(menuName = "AOP/Data/InGamePrefabSO", fileName = "AOP-Data-" + nameof(InGamePrefabsSO))]
    public class InGamePrefabsSO : IPrefabsContainerSO
    {
        public AssetReference GridWorldCellPrefab;
        public AssetReference InGameEnvironmentPrefab;

        public override void RegisterPrefabsToPool()
        {
            ObjectCamp.RegisterPrefab(new TypePrefabRegisterMap(typeof(GridWorldCell), GridWorldCellPrefab, PoolStaticVariations.VARIATION1));
            ObjectCamp.RegisterPrefab(new TypePrefabRegisterMap(typeof(InGameEnvironment), InGameEnvironmentPrefab, PoolStaticVariations.VARIATION1));

        }
    }
}

