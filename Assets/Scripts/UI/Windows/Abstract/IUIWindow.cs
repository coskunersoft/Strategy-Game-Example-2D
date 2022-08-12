using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using AOP.ObjectPooling;

namespace AOP.UI.Windows
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class IUIWindow: MonoBehaviour,IObjectCampMember
    {
        [FoldoutGroup("Window Variables")][SerializeField] protected List<IUIWindow> SubWindows;
        [FoldoutGroup("Window Variables")][ReadOnly] [SerializeField]private CanvasGroup canvasGroup;
        [FoldoutGroup("Window Variables")][ReadOnly]public bool isDisplaying = false;

        private void Awake()
        {
            isDisplaying = false;
            gameObject.TryGetComponent(out canvasGroup);
        }
        
        public virtual List<IUIWindow> CurrentSubWindow()
        {
            if (SubWindows.Count <= 0) return null;
            var finded = SubWindows.FindAll(x => x.isDisplaying);
            return finded;
        }
        public T GetSubWindow<T>() where T : IUIWindow
        {
            var finded = SubWindows.Find(x => x is T t);
            return finded as T;
        }
        protected virtual void DisplaySubWindow(IUIWindow subWindow,bool hideOthers=false)
        {
            if (subWindow.isDisplaying) return;
            if(hideOthers)
            SubWindows.ForEach(x => x.Hide(x.isDisplaying));
            subWindow.Show(true);
        }
        public virtual void DisplaySubWindowWithIndex(int index,bool hideOthers=false)
        {
            DisplaySubWindow(SubWindows[index],hideOthers);
        }
        public virtual void DisplaySubWindowWithType<T>(bool hideOthers = false) where T : IUIWindow
        {
            var finded = GetSubWindow<T>();
            if (finded)
                DisplaySubWindow(finded,hideOthers);
        }

        public virtual async void Hide(bool hitEvent=false,bool animated=true)
        {
            isDisplaying = false;
            if (animated)
                await canvasGroup.DOFade(0, 1f).AsyncWaitForCompletion();
            else canvasGroup.alpha = 0;
            ObjectCamp.PushObject(this);
        }
        public virtual async void Show(bool hitEvent = false, bool animated = true)
        {
            isDisplaying = true;
            if (animated)
                await canvasGroup.DOFade(1, 1f).AsyncWaitForCompletion();
            else canvasGroup.alpha = 1;
        }
    }
}

