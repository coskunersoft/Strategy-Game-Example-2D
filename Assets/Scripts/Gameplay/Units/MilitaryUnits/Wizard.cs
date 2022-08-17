using System.Collections;
using System.Collections.Generic;
using AOP.GamePlay.ChangeableSystems;
using AOP.GamePlay.Navigation;
using AOP.GamePlay.ChangeableSystems.Health;
using UnityEngine;
using AOP.DataCenter;

namespace AOP.GamePlay.Units
{
    [RequireComponent(typeof(NavigationAgent))]
    public class Wizard : IGameMilitaryUnit
    {
        private FireDamage fireDamage = new FireDamage();
        protected override IDamage AttackDamage => fireDamage;
      

    }
}