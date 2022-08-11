using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.Management
{
    public abstract class IManager : MonoBehaviour
    {
        public abstract IEnumerator Init();
    }
}
