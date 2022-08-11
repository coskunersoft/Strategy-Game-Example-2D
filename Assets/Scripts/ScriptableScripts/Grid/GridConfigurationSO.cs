using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.GridSystem;

namespace AOP.DataCenter
{
    [CreateAssetMenu(menuName = "AOP/Data/"+ nameof(GridConfigurationSO), fileName = "AOP-Data-" + nameof(GridConfigurationSO))]
    public class GridConfigurationSO : IGameSO
    {
        public List<CellGroundTypeSpriteMap> cellGroundTypeSpriteMaps;
    }
}
