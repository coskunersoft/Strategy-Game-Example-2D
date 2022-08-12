using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.GridSystem;
using AOP.GamePlay;
using AOP.ObjectPooling;
using UnityEngine.AddressableAssets;
using AOP.EventFactory;
using AOP.FunctionFactory;
using AOP.DataCenter;

namespace AOP.Management
{
    public class GameManager : MonoBehaviour
    {
        public List<IManager> SubManagers;
        private GameSquance CurrentGameSquance;

        #region Mono Functions
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            StartCoroutine(GameInitialization());

        }
        private void OnEnable()
        {
            Functions.General.RunCourotineInCenter += RunCourotineInCenter;
            Events.UIEvents.OnGameLevelSelectedButtonClick += OnGameLevelSelectedButtonClick;
        }
        private void OnDisable()
        {
            Functions.General.RunCourotineInCenter -= RunCourotineInCenter;
            Events.UIEvents.OnGameLevelSelectedButtonClick -= OnGameLevelSelectedButtonClick;

        }
        #endregion

        #region GameManager Bussiness
        private IEnumerator GameInitialization()
        {
            Events.GeneralEvents.OnGameInitializationStep?.Invoke(GameInitiazationSteps.GameAwake);

            foreach (var item in SubManagers)
                yield return item.Init();

            Events.GeneralEvents.OnGameInitializationStep?.Invoke(GameInitiazationSteps.ShowEntryWindow);
            yield return new WaitForSeconds(3);
            Events.GeneralEvents.OnGameInitializationStep?.Invoke(GameInitiazationSteps.GameInitializationDone);
        }

        public void LoadLevel(GameLevelSO gameLevelSO)
        {
            CurrentGameSquance = new GameSquance();
            StartCoroutine(CurrentGameSquance.Init(gameLevelSO));
        }
        #endregion

        #region Event/Function Listeners
        private void RunCourotineInCenter(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }
        private void OnGameLevelSelectedButtonClick(GameLevelSO gameLevelSO)
        {
            LoadLevel(gameLevelSO);
        }
        #endregion


    }
}
