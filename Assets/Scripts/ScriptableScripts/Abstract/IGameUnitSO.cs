using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AOP.DataCenter
{
    public abstract class IGameUnitSO : IGameSO
    {
        public string UnitName;
        public string UnityDesc;
        public Sprite UnitIcon;
        public AssetReference Prefab;
    }
}