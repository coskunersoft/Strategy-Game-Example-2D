using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;
using AOP.Utils;

namespace AOP.ObjectPooling
{
    public class ObjectCamp
    {
        public readonly static List<ObjectStock> objectStocks;
        public readonly static List<TypePrefabRegisterMap> typePrefabRegisterMaps;
        static ObjectCamp()
        {
            if (typePrefabRegisterMaps == null) typePrefabRegisterMaps = new List<TypePrefabRegisterMap>();
            if (objectStocks == null) objectStocks = new List<ObjectStock>();
        }
        public static void PushObject<T>(T Object ,string _variation="" ,params string[] parameters) where T : IObjectCampMember 
        {
            if (Object==null) return;
            if (!(Object is MonoBehaviour)) return;
            var Finded=objectStocks.Find(x => x.IsMatch(parameters)&&x.CanPushObject(typeof(T)));
            if (Finded == null)
            {
                
                Finded = new ObjectStock(parameters);
                Finded.Objects.Add(new ObjectStock.StockObjectItem() { ObjectCampMember = Object, variation = _variation });
                objectStocks.Add(Finded);
            }
            else
            {
                if (Finded.IsObjectInPool(Object))
                {
                    throw new Exceptions.ObjectAlreadyInObjectCampException((Object as MonoBehaviour).transform.name);
                }

                Finded.Objects.Add(new ObjectStock.StockObjectItem { variation=_variation, ObjectCampMember=Object });
            }
            MonoBehaviour behaviour = Object as MonoBehaviour;
            behaviour.gameObject.SetActive(false);
        }
        public static async Task<T> PullObject<T>(string variation="",Transform carrier=null, params string[] parameters) where T : IObjectCampMember
        {
            var Finded = objectStocks.Find(x => x.IsMatch(parameters) && x.CanPullObject(typeof(T),variation));
            if (Finded != null)
            {
                var Object= (T)Finded.GetObject();
                MonoBehaviour behaviour = Object as MonoBehaviour;
                behaviour.gameObject.SetActive(true);
                behaviour.transform.SetParent(carrier);
                return Object;
            }
            else
            {
                var findedRegisterMap= typePrefabRegisterMaps.Find(x => x.type == typeof(T)&&x.variation.Equals(variation,System.StringComparison.OrdinalIgnoreCase));
                if (findedRegisterMap == null)
                    throw new Exceptions.ObjectTypePrefabNotRegisteredException(typeof(T));
                
                var task = findedRegisterMap.assetReference.AddressableInstantiate(carrier);
                await task;
                task.Result.TryGetComponent(out T Component);
                MonoBehaviour behaviour = Component as MonoBehaviour;
                behaviour.gameObject.SetActive(true);
                return Component;
            }
           
        }
        public static void RegisterPrefab(TypePrefabRegisterMap typePrefabRegisterMap)
        {
            if (!typePrefabRegisterMap.type.GetInterfaces().Any(x => x == typeof(IObjectCampMember)))
                throw new Exceptions.ObjectCampRegisterException(typePrefabRegisterMap.type,0);
            if (typePrefabRegisterMaps.Any(x => x.type == typePrefabRegisterMap.type&&x.variation.Equals(typePrefabRegisterMap.variation, System.StringComparison.OrdinalIgnoreCase)))
                throw new Exceptions.ObjectCampRegisterException(typePrefabRegisterMap.type,1);
            typePrefabRegisterMaps.Add(typePrefabRegisterMap);
        }
    }
}

