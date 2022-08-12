using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AOP.Management.Scene;

namespace AOP.DataCenter.Scene
{
    [CreateAssetMenu(fileName = "AOP-Scene-Data", menuName = "AOP/Data/SceneData")]
    public class SceneDataSO : IGameSO
    {
        public List<MasterSceneMap> masterSceneMaps;
    }
}
