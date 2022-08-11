using System;

namespace AOP.Exceptions
{
    public class ObjectAlreadyInObjectCampException : Exception
    {
        public ObjectAlreadyInObjectCampException(string objectName)
               : base("<b><color=red> " + objectName + " is already in Camp  </color></b> ")
        {

        }
    }
}
