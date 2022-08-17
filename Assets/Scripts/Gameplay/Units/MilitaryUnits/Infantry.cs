using System.Collections;
using System.Collections.Generic;
using AOP.GamePlay.ChangeableSystems;
using AOP.GamePlay.ChangeableSystems.Health;
using AOP.GamePlay.Navigation;
using UnityEngine;


namespace AOP.GamePlay.Units
{
    [RequireComponent(typeof(NavigationAgent))]
    public class Infantry : IGameMilitaryUnit
    {
        private FireDamage fireDamage = new FireDamage();
        protected override IDamage AttackDamage => fireDamage;
    }
}
