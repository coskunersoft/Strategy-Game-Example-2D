using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using AOP.ObjectPooling;
using AOP.EventFactory;

namespace AOP.UI.Windows
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class IUIWindow: IUIDisplayable, IObjectCampMember
    {
        [FoldoutGroup("Window Variables")][SerializeField] protected List<IUISubWindow> SubWindows;
       
        
        public virtual List<IUISubWindow> CurrentSubWindow()
        {
            if (SubWindows.Count <= 0) return null;
            var finded = SubWindows.FindAll(x => x.isDisplaying);
            return finded;
        }
        public T GetSubWindow<T>() where T : IUISubWindow
        {
            var finded = SubWindows.Find(x => x is T);
            return finded as T;
        }
        protected virtual void DisplaySubWindow(IUISubWindow subWindow,bool hideOthers=false)
        {
            if (subWindow.isDisplaying) return;
            if(hideOthers)
            SubWindows.ForEach(x => x.Hide(x.isDisplaying));
            subWindow.Show(true);
            Debug.Log("Sub Displayed");
        }
        public virtual void DisplaySubWindowWithIndex(int index,bool hideOthers=false)
        {
            DisplaySubWindow(SubWindows[index],hideOthers);
        }
        public virtual void DisplaySubWindowWithType<T>(bool hideOthers = false) where T : IUISubWindow
        {
            var finded = GetSubWindow<T>();
            if (finded)
                DisplaySubWindow(finded,hideOthers);
        }

        protected override IEnumerator HideSquence(bool hitEvent=false,bool animated=true)
        {
           yield return base.HideSquence(hitEvent, animated);
            ObjectCamp.PushObject(this);
            if (hitEvent)
                Events.UIEvents.OnAnyMasterWindowShow?.Invoke(this);
        }
        protected override IEnumerator ShowSquence(bool hitEvent = false, bool animated = true)
        {
            yield return base.ShowSquence(hitEvent, animated);
            if (hitEvent)
                Events.UIEvents.OnAnyMasterWindowHide?.Invoke(this);
        }
    }
}

