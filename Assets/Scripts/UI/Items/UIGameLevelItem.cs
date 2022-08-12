using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using AOP.ObjectPooling;
using AOP.UI.DataContainers;
using AOP.DataCenter;

namespace AOP.UI.Items
{
    public class UIGameLevelItem :MonoBehaviour, IUIDataContainer<GameLevelSO>, IObjectCampMember
    {
        public Image LevelIcon;
        public TextMeshProUGUI LevelTitleText;
        public Button Button;

        public void ApplyData(GameLevelSO Data)
        {
            LevelIcon.sprite = Data.LevelIcon;
            LevelTitleText.text = Data.LevelTitle;

        }
    }
}