using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AOP.DataCenter
{

    [CreateAssetMenu(menuName = "AOP/Data/InGamePrefabSO", fileName = "AOP-Data-" + nameof(InGamePrefabsSO))]
    public class InGamePrefabsSO : IGameSO
    {
        public AssetReference GridWorldCellPrefab;
    }
}

