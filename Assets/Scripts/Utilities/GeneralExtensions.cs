using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.Extensions
{
    public static class GeneralExtensions
    {
        public static Vector2 CenterOfVectors(this List<Vector2> vectors)
        {
            Vector2 result = default;
            for (int i = 0; i < vectors.Count; i++)
                result += vectors[i];
            result /= vectors.Count;
            return result;
        }
    }
}

