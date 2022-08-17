using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AOP.GamePlay.ChangeableSystems
{
    public abstract class IChangableSystem<Positive, Negative> : MonoBehaviour where Positive : IChangebleSystemValue where Negative : IChangebleSystemValue
    {
        public int Amount;
        public int MaxAmount;
        public float AmountPercent => ((float)Amount / (float)MaxAmount) * 100;
        public bool isEmpty => Amount <= 0;
        public bool isFull => Amount >= MaxAmount;

        #region Health Actions
        public Action onAmountChanged;

        public Action<Negative> onTakeAmountDamage;
        public Action<Negative> onTakeAmountDamageInEmpty;
        public Action<Negative> onAmountEmpty;

        public Action<Positive> onTakeHealAmount;
        public Action<Positive> ontakeHealAmountWhenFull;
        #endregion

        public virtual void TakeDamage(Negative damage)
        {
            if (isEmpty)
            {
                onTakeAmountDamageInEmpty?.Invoke(damage);
                return;
            }

            Amount -= damage;
            onAmountChanged?.Invoke();
            onTakeAmountDamage?.Invoke(damage);
            if (Amount <= 0)
            {
                Amount = 0;
                onAmountEmpty?.Invoke(damage);

            }
        }
        public virtual void TakeHeal(Positive heal)
        {
            if (isFull)
            {
                ontakeHealAmountWhenFull?.Invoke(heal);
                return;
            }
            Amount += heal;
            onAmountChanged?.Invoke();
            onTakeHealAmount?.Invoke(heal);
            if (Amount > MaxAmount)
            {
                Amount = MaxAmount;
            }
        }

        public virtual void InitAmount(int amount)
        {
            Amount = amount;
            MaxAmount = amount;
        }

    }
}