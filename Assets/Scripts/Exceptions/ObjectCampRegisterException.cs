using System;

namespace AOP.Exceptions
{
    public class ObjectCampRegisterException : Exception
    {
        
        public ObjectCampRegisterException( Type type, int exceptionType = 0)
                  : base(exceptionType==0?MessageOne(type):MessageTwo(type))
        {

        }

        private static string MessageOne(Type type)
        {
            return "<b><color=red> " + type + " cant registered you have to add  " + nameof(ObjectPooling.IObjectCampMember) + " interface to this class</color></b> ";
        }
        private static string MessageTwo(Type type)
        {
            return "<b><color=red> This type ("+type+") already regitered </color></b> ";
        }
    }
}