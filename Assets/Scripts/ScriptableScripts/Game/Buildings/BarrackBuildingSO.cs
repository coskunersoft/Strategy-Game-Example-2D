using System.Collections;
using System.Collections.Generic;
using AOP.UI.DataContainers;
using UnityEngine;
using AOP.GamePlay.Units;

namespace AOP.DataCenter
{
    [CreateAssetMenu(fileName = "AOP-Game-Building-Barrack-", menuName = "AOP/Units/" + nameof(BarrackBuildingSO))]
    public class BarrackBuildingSO : BuildingSO
    {
        public List<MilitaryUnitSO> ProductionMilitaryUnits;
    }
}
