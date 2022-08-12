using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.UI.DataContainers;

namespace AOP.DataCenter
{
    [CreateAssetMenu(fileName = "AOP-Game-Data", menuName = "AOP/Data/" + nameof(GameDataSO))]
    public class GameDataSO : IGameSO,IUIContainerData
    {
        public List<GameLevelSO> gameLevels;
    }
}
