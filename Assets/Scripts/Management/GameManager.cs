using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.GridSystem;
using AOP.GamePlay;
using AOP.ObjectPooling;
using UnityEngine.AddressableAssets;
using AOP.EventFactory;

namespace AOP.Management
{
    public class GameManager : MonoBehaviour
    {
        public List<IManager> SubManagers;
        private GameSquance CurrentGameSquance;


        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            StartCoroutine(GameInitialization());

        }

        private IEnumerator GameInitialization()
        {
            Events.General.OnGameInitializationStep?.Invoke(GameInitiazationSteps.GameAwake);

            foreach (var item in SubManagers)
                yield return item.Init();

            Events.General.OnGameInitializationStep?.Invoke(GameInitiazationSteps.ShowEntiyWindow);
            yield return new WaitForSeconds(10);
            Events.General.OnGameInitializationStep?.Invoke(GameInitiazationSteps.EndEntityWindow);

        

        }

        public void LoadGame()
        {
            CurrentGameSquance = new GameSquance();
            CurrentGameSquance.Init();

        }

      
    }
}
