using System.Collections;
using System.Collections.Generic;
using AOP.DataCenter;
using AOP.GamePlay.Units;
using UnityEngine;
using AOP.UI.DataContainers;

namespace AOP.GamePlay.DataMaps
{
    public class MilitaryProductionItemData :IUIContainerData
    {
        public MilitaryUnitSO militaryUnitSO;
        public IGameBarrackBuildingUnit gameBarrackBuildingUnit;
    }
}