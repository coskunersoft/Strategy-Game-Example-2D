using System.Collections;
using System.Collections.Generic;
using AOP.UI.DataContainers;
using UnityEngine;
using AOP.GamePlay.Units;

namespace AOP.DataCenter
{
    [CreateAssetMenu(fileName = "AOP-Game-Building", menuName = "AOP/Units/" + nameof(BuildingSO))]
    public class BuildingSO : IGameUnitSO, IUIContainerData
    {
        public BuildingOrtiantationProfileSO buildingOrtiantationProfileSO;
    }
}
