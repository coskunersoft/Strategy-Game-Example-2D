using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.DataCenter
{
    [CreateAssetMenu(menuName = "AOP/Data/PrefabSO", fileName = "AOP-Data-" + nameof(PrefabsSO))]
    public class PrefabsSO : IPrefabsContainerSO
    {
        [SerializeField] private List<IPrefabsContainerSO> registerPrefabContainerSOList;
    
        [SerializeField]private InGamePrefabsSO InGamePrefabsSO;

        public override void RegisterPrefabs()
        {
            registerPrefabContainerSOList.ForEach(x => x.RegisterPrefabs());
        }
    }
}
