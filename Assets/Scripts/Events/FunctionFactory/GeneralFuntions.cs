using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace AOP.FunctionFactory
{
    public static partial class Functions 
    {
        public static class General
        {
            public static Action<IEnumerator> RunCourotineInCenter;
        }
    }
}
