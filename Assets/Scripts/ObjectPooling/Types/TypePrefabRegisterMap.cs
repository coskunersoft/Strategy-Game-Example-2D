using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace AOP.ObjectPooling
{
    [System.Serializable]
    public class TypePrefabRegisterMap
    {
        public readonly System.Type type;
        public readonly string variation;
        public readonly AssetReference assetReference;

        public TypePrefabRegisterMap(System.Type type,AssetReference assetReference,string variation="")
        {
            this.assetReference = assetReference;
            this.type = type;
            this.variation = variation;
        }
    }
}

