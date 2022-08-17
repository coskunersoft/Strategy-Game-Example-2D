using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.UI.DataContainers;
using AOP.ObjectPooling;
using AOP.GamePlay.Units;
using AOP.GamePlay.FX;

namespace AOP.DataCenter
{
    [CreateAssetMenu(fileName = "AOP-Game-Data", menuName = "AOP/Data/" + nameof(GameDataSO))]
    public class GameDataSO : IPrefabsContainerSO,IUIContainerData
    {
        public List<GameLevelSO> gameLevels;
        public List<BuildingSO> allBuildings;
        public List<MilitaryUnitSO> allMilitaryUnits;
        public List<OneShotParticleSO> allOneShotParticles;

        public Color32 WrongUnitPlaceColor;
        public Color32 RightUnitPlaceColor;

        public Material UnitNormalMaterial;
        public Material UnitSelectedMaterial;

        public override void RegisterPrefabsToPool()
        {
            foreach (var item in allBuildings)
            {
                ObjectCamp.RegisterPrefab(new TypePrefabRegisterMap(typeof(IGameUnit),item.Prefab, item.UnitName));
            }
            foreach (var item in allMilitaryUnits)
            {
                ObjectCamp.RegisterPrefab(new TypePrefabRegisterMap(typeof(IGameUnit), item.Prefab, item.UnitName));
            }
            foreach (var item in allOneShotParticles)
            {
                ObjectCamp.RegisterPrefab(new TypePrefabRegisterMap(typeof(OneShotParticle), item.assetReference, item.ParticleName));
            }
        }
    }
}
