using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.UI.DataContainers;

namespace AOP.DataCenter
{
    [CreateAssetMenu(fileName = "AOP-Game-Level", menuName = "AOP/Data/" + nameof(GameLevelSO))]
    public class GameLevelSO : IGameSO,IUIContainerData
    {
        public string LevelID;
        public Sprite LevelIcon;
        public string LevelTitle;
        public int GridSize;
    }
}
