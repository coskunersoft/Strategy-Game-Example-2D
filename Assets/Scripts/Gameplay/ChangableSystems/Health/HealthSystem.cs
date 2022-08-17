using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AOP.GamePlay.ChangeableSystems.Health
{
    public class HealthSystem : IChangableSystem<IHeal, IDamage>
    {
        public override void TakeDamage(IDamage damage)
        {
            base.TakeDamage(damage);
        }
        public override void TakeHeal(IHeal heal)
        {
            base.TakeHeal(heal);
        }
    }
}

