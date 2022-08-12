using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.GridSystem;

namespace AOP.DataCenter
{
    [CreateAssetMenu(menuName = "AOP/Data/"+ nameof(GridConfigurationSO), fileName = "AOP-Data-" + nameof(GridConfigurationSO))]
    public class GridConfigurationSO : IGameSO
    {
        public float GridCellDistance;
        [Space(10)]
        public List<CellGroundTypeSpriteMap> cellGroundTypeSpriteMaps;
    }
}
