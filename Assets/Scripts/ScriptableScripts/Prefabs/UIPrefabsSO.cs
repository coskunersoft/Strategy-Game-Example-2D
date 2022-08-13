using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using AOP.ObjectPooling;
using AOP.UI;
using AOP.UI.Items;
using AOP.UI.Windows;

namespace AOP.DataCenter
{
    [CreateAssetMenu(fileName = "AOP-UI-UIPrefabsDataSO", menuName = "AOP/Data/UIPrefabsDataSO")]
    public class UIPrefabsSO : IPrefabsContainerSO
    {
        public AssetReference EntryWindowReferance;
        public AssetReference MainMenuWindowReferance;
        public AssetReference LoadingWindowReferance;
        public AssetReference InGameWindowReferance;

        public AssetReference UILevelItemReferance;
        public AssetReference UIBuildingItemReference;

        public override void RegisterPrefabsToPool()
        {
            ObjectCamp.RegisterPrefab(new TypePrefabRegisterMap(typeof(IUIWindow), EntryWindowReferance, WindowTitles.EntryWindow));
            ObjectCamp.RegisterPrefab(new TypePrefabRegisterMap(typeof(IUIWindow), MainMenuWindowReferance, WindowTitles.MainMenuWindow));
            ObjectCamp.RegisterPrefab(new TypePrefabRegisterMap(typeof(IUIWindow), LoadingWindowReferance, WindowTitles.LoadingWindow));
            ObjectCamp.RegisterPrefab(new TypePrefabRegisterMap(typeof(IUIWindow), InGameWindowReferance, WindowTitles.InGameWindow));


            ObjectCamp.RegisterPrefab(new TypePrefabRegisterMap(typeof(UIGameLevelItem), UILevelItemReferance));
            ObjectCamp.RegisterPrefab(new TypePrefabRegisterMap(typeof(UIGameBuildingItem), UIBuildingItemReference));

        }
    }
}
