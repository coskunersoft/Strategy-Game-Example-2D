using System;

namespace AOP.EventFactory
{
    public static partial class Events
    {
        public static class General
        {
            public static Action OnEntryWindowStarted;
            public static Action OnEntryWindowEnded;
            public static Action OnGameInitializationStarted;
            public static Action OnGameInitializationEnded;
            public static Action<int> OnGameInitializationStep;
        }
    }
}
