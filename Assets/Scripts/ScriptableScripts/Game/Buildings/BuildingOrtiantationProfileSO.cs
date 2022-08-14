using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace AOP.DataCenter
{
    [CreateAssetMenu(fileName = "AOP-Game-BuldingOrtiantationProfile", menuName = "AOP/Units/" + nameof(BuildingOrtiantationProfileSO))]
    public class BuildingOrtiantationProfileSO : IGameSO
    {
        public List<DirectionGroup> OriantationGroups;
    }
}
