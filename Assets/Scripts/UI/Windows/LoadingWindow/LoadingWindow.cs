using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace AOP.UI.Windows
{
    public class LoadingWindow : IUIWindow
    {
        [SerializeField]private Transform LodingIcon;
        private Tween animateTween;

        protected override IEnumerator ShowSquence(bool hitEvent = false, bool animated = false)
        {
            yield return base.ShowSquence(hitEvent, animated);
            animateTween = LodingIcon.transform.DORotate(Vector3.forward * 360, 1, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Restart);
        }
        protected override IEnumerator HideSquence(bool hitEvent = false, bool animated = false)
        {
            yield return base.HideSquence(hitEvent, animated);
            animateTween.Kill();
        }

    }
}