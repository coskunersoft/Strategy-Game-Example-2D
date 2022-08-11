using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.DataCenter;

namespace AOP.Management
{
    public class DataHolder : IManager
    {
        public PrefabsSO prefabsSO;

        public override IEnumerator Init()
        {
            RegisterPrefabs();
            yield return null;
        }

        private void RegisterPrefabs()
        {
        }
    }
}