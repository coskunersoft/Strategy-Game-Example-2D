using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.GamePlay.ChangeableSystems
{
    public abstract class IChangebleSystemValue
    {
        public int amount;
        public static implicit operator int(IChangebleSystemValue value) => value.amount;
    }
}
