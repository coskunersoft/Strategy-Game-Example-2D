using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AOP.DataCenter;

namespace AOP.ObjectPooling
{
    public class TypeScriptableObjectMap
    {
        public readonly Type type;
        public readonly IGameSO gameSO;
        public readonly string variation;

        public TypeScriptableObjectMap(Type type,IGameSO gameSO,string variation)
        {
            this.type = type;
            this.gameSO = gameSO;
            this.variation = variation;
        }
    }
}
