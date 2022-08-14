using System.Collections;
using System.Collections.Generic;
using AOP.UI.DataContainers;
using UnityEngine;

namespace AOP.DataCenter
{
    [CreateAssetMenu(fileName = "AOP-Game-MilitaryUnit-", menuName = "AOP/Units/" + nameof(MilitaryUnitSO))]
    public class MilitaryUnitSO : IGameUnitSO, IUIContainerData
    {
        public int StrikePower;
        public int MaxHealth;
    }
}
