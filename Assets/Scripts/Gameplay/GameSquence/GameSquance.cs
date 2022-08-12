using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.GridSystem;

namespace AOP.GamePlay
{
    public class GameSquance
    {
        private GameGrid GameGrid;
        public void Init()
        {
            GameGrid = new GameGrid(10, Vector2.zero);
        }
    }
}
