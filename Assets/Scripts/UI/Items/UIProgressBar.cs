using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AOP.UI.Items
{
    public class UIProgressBar : MonoBehaviour
    {
        public Image fillingImage;

        public void SetProgress(float progress)
        {
            fillingImage.fillAmount = progress;
        }
    }
}
