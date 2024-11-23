using UnityEngine;
using PathCreation;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Onion_AI
{
    [System.Serializable]
    public class JetStatistics
    {
        public float topSpeed;
        public float healthCapacity;
        public float bulletDamageValue;
    }

    [System.Serializable]
    public class PathControllerClass
    {
        public string wayPointName;
        public PathCreator[] pathControllers;
    }
}
