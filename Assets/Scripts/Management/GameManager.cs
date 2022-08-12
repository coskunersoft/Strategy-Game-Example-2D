using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.GridSystem;
using AOP.GamePlay;

namespace AOP.Management
{
    public class GameManager : MonoBehaviour
    {
        public List<IManager> SubManagers;
        private GameSquance CurrentGameSquance;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            StartCoroutine(GameInit());
        }

        private IEnumerator GameInit()
        {
            foreach (var item in SubManagers)
                yield return item.Init();
        }

        public void LoadGame()
        {
            CurrentGameSquance = new GameSquance();
            CurrentGameSquance.Init();
        }
    }
}
