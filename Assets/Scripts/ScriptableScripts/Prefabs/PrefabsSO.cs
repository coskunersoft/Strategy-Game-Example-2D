using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.DataCenter
{
    [CreateAssetMenu(menuName = "AOP/Data/PrefabSO", fileName = "AOP-Data-" + nameof(PrefabsSO))]
    public class PrefabsSO : IGameSO
    {
        [SerializeField] private List<IPrefabsContainerSO> registerPrefabContainerSOList;
     
        public void RegisterPrefabsToPool()
        {
            registerPrefabContainerSOList.ForEach(x => x.RegisterPrefabsToPool());
        }
    }
}
