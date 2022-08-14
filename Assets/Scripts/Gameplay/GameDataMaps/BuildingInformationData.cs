using System.Collections;
using System.Collections.Generic;
using AOP.DataCenter;
using AOP.GamePlay.Units;
using AOP.UI.DataContainers;
using UnityEngine;

namespace AOP.GamePlay.DataMaps
{
    public class BuildingInformationData :IUIContainerData
    {
        public BuildingSO buildingSO;
        public IGameBuildingUnit gameBuildingUnit;
    }
}