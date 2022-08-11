using System;

namespace AOP.Exceptions
{
    public class ObjectTypePrefabNotRegisteredException : Exception
    {
        public ObjectTypePrefabNotRegisteredException(Type type)
              : base("<b><color=red> " + type + " </color></b> Not Registered In Object Pool (Object Camp)")
        {

        }
    }
}
