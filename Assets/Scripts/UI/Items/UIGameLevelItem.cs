using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using AOP.ObjectPooling;
using AOP.UI.DataContainers;
using AOP.DataCenter;
using AOP.EventFactory;

namespace AOP.UI.Items
{
    public class UIGameLevelItem :MonoBehaviour, IUIDataContainer<GameLevelSO>, IObjectCampMember
    {
        public Image LevelIcon;
        public TextMeshProUGUI LevelTitleText;
        public Button Button;
        private GameLevelSO currentLevelSO;

        public void ApplyData(GameLevelSO Data)
        {
            currentLevelSO = Data;
            LevelIcon.sprite = Data.LevelIcon;
            LevelTitleText.text = Data.LevelTitle;
            this.Button.onClick = new Button.ButtonClickedEvent();
            Button.onClick.AddListener(ClickMe);
        }

        private void ClickMe()
        {
            if (!currentLevelSO) return;
            Events.UIEvents.OnGameLevelSelectedButtonClick?.Invoke(currentLevelSO);
        }
    }
}