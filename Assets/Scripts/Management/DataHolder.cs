using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.DataCenter;
using AOP.ObjectPooling;


namespace AOP.Management
{
    public class DataHolder : IManager
    {
        [SerializeField]private List<IGameSO> RegisterGameSOList;
        public PrefabsSO prefabsSO;
        
        public override IEnumerator Init()
        {
            prefabsSO.RegisterPrefabs();
            RegisterGameSOList.ForEach(x => ObjectCamp.RegisterScriptable(x));
            yield return null;
        }
    }
}