using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AOP.GamePlay.Navigation;

namespace AOP.Exceptions
{
    public class NavigationAgentNotInitializedException : Exception
    {
        public NavigationAgentNotInitializedException(NavigationAgent navigationAgent)
            : base("To use Pathfinding you have to initialize Navigation Agent <b> (" + navigationAgent.transform.name+")</b>")
        {

        }
    }

}