using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.GridSystem
{
    public class GridCell
    {
        private CellGroundType cellGroundType;
        public CellGroundType CellGroundType { get;private set; }
        
        public void SelectGroundType(CellGroundType cellGroundType)
        {
            CellGroundType = cellGroundType;
            Visualize();
        }

        public void Visualize()
        {

        }
    }
}
