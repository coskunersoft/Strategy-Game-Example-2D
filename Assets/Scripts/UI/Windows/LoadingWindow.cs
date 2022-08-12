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

        public override void Show(bool hitEvent = false, bool animated = false)
        {
            base.Show(hitEvent, animated);
            animateTween = LodingIcon.transform.DORotate(Vector3.forward * 360, 1, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Restart);
        }
        public override void Hide(bool hitEvent = false, bool animated = false)
        {
            base.Hide(hitEvent, animated);
            animateTween.Kill();
        }

    }
}