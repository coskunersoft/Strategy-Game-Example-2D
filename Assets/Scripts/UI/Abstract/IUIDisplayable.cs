using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AOP.ObjectPooling;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using AOP.FunctionFactory;

namespace AOP.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class IUIDisplayable:MonoBehaviour
    {
        [FoldoutGroup("Displayable Variables")] [ReadOnly] public CanvasGroup canvasGroup;
        [FoldoutGroup("Displayable Variables")] [ReadOnly] public bool isDisplaying = false;

        protected virtual void Awake()
        {
            isDisplaying = false;
            TryGetComponent(out canvasGroup);
        }

        public void Show(bool hitEvent = false, bool animated = true)
        {
            Functions.General.RunCourotineInCenter?.Invoke(ShowSquence(hitEvent,animated));
        }
        public void Hide(bool hitEvent = false, bool animated = true)
        {
            Functions.General.RunCourotineInCenter?.Invoke(HideSquence(hitEvent, animated));
        }
        protected virtual IEnumerator HideSquence(bool hitEvent = false, bool animated = true)
        {
            isDisplaying = false;
            if (animated)
                yield return canvasGroup.DOFade(0, .5f).WaitForCompletion();
            else canvasGroup.alpha = 0;

            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
        protected virtual IEnumerator ShowSquence(bool hitEvent = false, bool animated = true)
        {
            isDisplaying = true;
            if (animated)
                yield return canvasGroup.DOFade(1, .5f).WaitForCompletion();
            else canvasGroup.alpha = 1;

            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
    }
}
