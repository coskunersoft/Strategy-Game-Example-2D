using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.ObjectPooling;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AOP.Utils
{
    public static partial class Utilities
    {
        /// <summary>
        /// Pulls an object back into the pool
        /// </summary>
        /// <param name="objectCampMember"></param>
        public static void PushToCamp(this IObjectCampMember objectCampMember)
        {
            ObjectCamp.PushObject(objectCampMember);
        }
        /// <summary>
        /// Creates the asset reference object
        /// </summary>
        /// <param name="assetReference"></param>
        /// <param name="Objectparent"></param>
        /// <returns></returns>
        public static async Task<GameObject> AddressableInstantiate(this AssetReference assetReference, Transform Objectparent = null)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(assetReference, parent: Objectparent);
            await handle.Task;
            return handle.Result;
        }
        /// <summary>
        /// Matches whether two classes are from the same family
        /// </summary>
        /// <param name="type"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool IsSameFamily(this System.Type type, System.Type other)
        {
            System.Type tempType = type;
            System.Type tempOther = other;
            bool searchResult;
            while (true)
            {
                searchResult = tempType == tempOther;
                if (!searchResult) tempOther = tempOther.BaseType;
                if (searchResult) break;
                if (tempOther == null) break;
            }
            if (!searchResult)
            {
                tempOther = other;
                while (true)
                {
                    searchResult = tempType == tempOther;
                    if (!searchResult) tempType = tempType.BaseType;
                    if (searchResult) break;
                    if (tempType == null) break;
                }
            }
            return searchResult;
        }

    }
}
