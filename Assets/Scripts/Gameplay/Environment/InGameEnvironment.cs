using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.ObjectPooling;
using AOP.GamePlay.CameraControl;

namespace AOP.GamePlay
{
    public class InGameEnvironment : MonoBehaviour,IObjectCampMember
    {
        public Transform GridStartPoint;
        public CameraController CameraControl;
    }
}
