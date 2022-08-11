using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.Management
{
    public class GameManager : MonoBehaviour
    {
        public List<IManager> SubManagers;

        private void Awake()
        {
            StartCoroutine(GameInit());
        }

        private IEnumerator GameInit()
        {
            foreach (var item in SubManagers)
                yield return item.Init();
        }
    }
}
