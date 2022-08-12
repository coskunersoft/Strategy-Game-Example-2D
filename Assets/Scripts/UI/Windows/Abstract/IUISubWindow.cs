using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.EventFactory;

namespace AOP.UI.Windows
{
    public class IUISubWindow : IUIDisplayable
    {
       
        protected override IEnumerator HideSquence(bool hitEvent = false, bool animated = true)
        {
            yield return base.HideSquence(hitEvent, animated);
            if (hitEvent)
                Events.UIEvents.OnAnySubWindowHide?.Invoke(this);
        }

        protected override IEnumerator ShowSquence(bool hitEvent = false, bool animated = true)
        {
            yield return base.ShowSquence(hitEvent,animated);
            if (hitEvent)
                Events.UIEvents.OnAnySubWindowShow?.Invoke(this);
        }
    }
}
