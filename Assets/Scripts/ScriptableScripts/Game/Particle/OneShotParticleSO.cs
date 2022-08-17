using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AOP.DataCenter
{

    [CreateAssetMenu(fileName = "AOP-Game-Particle", menuName = "AOP/Data/" + nameof(OneShotParticleSO))]
    public class OneShotParticleSO : IGameSO
    {
        public string ParticleName;
        public AssetReference assetReference;
    }
}
