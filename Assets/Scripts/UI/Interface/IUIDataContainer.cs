using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.UI.DataContainers
{
    public interface IUIDataContainer<T>  where T:IUIContainerData
    {
        public abstract void ApplyData(T Data);
    }
}