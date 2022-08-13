using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.DataCenter;
using AOP.ObjectPooling;
using Sirenix.OdinInspector;


namespace AOP.Management
{
    public class DataManager : IManager
    {
        [SerializeField]private List<IGameSO> PoolRegisterGameSOList;
        public PrefabsSO prefabsSO;

        public override IEnumerator Init()
        {
            prefabsSO.RegisterPrefabsToPool();
            PoolRegisterGameSOList.ForEach(x => ObjectCamp.RegisterScriptable(x));
            yield return null;
        }
    }
}