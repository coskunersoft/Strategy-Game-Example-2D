using System.Collections;
using System.Collections.Generic;
using AOP.UI.DataContainers;
using UnityEngine;

namespace AOP.DataCenter
{
    [CreateAssetMenu(fileName = "AOP-Game-MilitaryUnit-", menuName = "AOP/Units/" + nameof(MilitaryUnitSO))]
    public class MilitaryUnitSO : IGameUnitSO, IUIContainerData
    {
        [Range(1, 20)]
        public int AttackDistance;
        [Range(0.5f,10)]
        public float AttackRate=4;
        public int StrikePower;
        public OneShotParticleSO HitToEnemyParticleSO;
    }
}
