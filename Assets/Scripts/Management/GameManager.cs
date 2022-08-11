using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.GridSystem;

namespace AOP.Management
{
    public class GameManager : MonoBehaviour
    {
        public List<IManager> SubManagers;

        private GameGrid gameGrid;

        private void Awake()
        {
            StartCoroutine(GameInit());
        }

        private void Start()
        {
            gameGrid = new GameGrid(10,new Vector2(0,0));
        }

        private IEnumerator GameInit()
        {
            foreach (var item in SubManagers)
                yield return item.Init();


        }
    }
}
