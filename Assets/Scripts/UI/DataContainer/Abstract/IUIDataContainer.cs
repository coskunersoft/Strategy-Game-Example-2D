using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.UI.DataContainers
{
    public abstract class IUIDataContainer<T> : MonoBehaviour where T:IUIContainerData
    {
        public abstract void ApplyData(T Data);
    }
}