using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using AOP.Utils;

namespace AOP.ObjectPooling
{
    public class ObjectStock
    {
        public readonly string[] Parameters;
        public readonly List<StockObjectItem> Objects;
        public ObjectStock(params string[] parameters)
        {
            Objects = new List<StockObjectItem>();
            Parameters = parameters;
        }
        public bool IsMatch(params string[] parameters)
        {
            return Parameters.Except(parameters).Count()<=0;
        }
        public bool IsObjectInPool(IObjectCampMember objectCampMember)
        {
            return Objects.Any(x=>x==objectCampMember);
        }
        public bool CanPushObject(System.Type otherType)
        {
            if (Objects.Count <= 0) return true;
            System.Type type = Objects.FirstOrDefault().ObjectCampMember.GetType();
            return type.IsSameFamily(otherType);
            
        }
        public bool CanPullObject(System.Type otherType,string variation="")
        {
            if (Objects.Count <= 0) return false;
            System.Type type = Objects.FirstOrDefault().ObjectCampMember.GetType();
            return type.IsSameFamily(otherType) && Objects.Any(x => x.variation.Equals(variation, System.StringComparison.OrdinalIgnoreCase));
        }
        public IObjectCampMember GetObject()
        {
            var finded = Objects.FirstOrDefault();
            Objects.Remove(finded);
            return finded.ObjectCampMember;
        }

        public class StockObjectItem
        {
            public string variation;
            public IObjectCampMember ObjectCampMember;
        }

    }
}

