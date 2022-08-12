using System;
using AOP.Management.Scene;

namespace AOP.EventFactory
{
    public static partial class Events
    {
        public static class SceneEvents 
        {
            public static Action<MasterSceneType> OnAnyMasterSceneLoadingStarted;
            public static Action<MasterSceneType> OnAnyMasterSceneLoadingCompeted;
        }
    }
}
