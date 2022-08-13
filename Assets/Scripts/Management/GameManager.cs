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
using System;
using AOP.GamePlay.Squance;

namespace AOP.Management
{
    public class GameManager : MonoBehaviour
    {
        public List<IManager> SubManagers;
        private GameSquance CurrentGameSquance;
        private Action UpdateAction;

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
        private void Update()
        {
            UpdateAction?.Invoke();
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

        public IEnumerator LoadLevel(GameLevelSO gameLevelSO)
        {
            CurrentGameSquance = new GameSquance();
            CurrentGameSquance.SubscribeEvents();
            yield return CurrentGameSquance.Load(gameLevelSO);
            UpdateAction += CurrentGameSquance.GameUpdate;
        }
        #endregion

        #region Event/Function Listeners
        private void RunCourotineInCenter(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }
        private void OnGameLevelSelectedButtonClick(GameLevelSO gameLevelSO)
        {
            StartCoroutine(LoadLevel(gameLevelSO));
        }
        #endregion


    }
}
