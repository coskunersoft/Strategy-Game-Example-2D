using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.GamePlay.Squance
{
    public abstract class IGameSquanceJob 
    {
        public bool IsStarted { get; private set; }
        public bool IsCanceled { get; private set; }
        public IGameSquanceJob()
        {
            IsStarted = false;
        }
        public abstract bool CompleteRule();
        public abstract void Continue();
        public abstract void Started();
        public abstract void Completed();
        public abstract void Canceled();

        public void StartJob()
        {
            IsStarted = true;
            Started();
        }
        public void CancelJob()
        {
            IsCanceled = true;
            Canceled();
        }
       
    }

}
