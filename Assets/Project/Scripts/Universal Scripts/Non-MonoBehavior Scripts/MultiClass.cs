using UnityEngine;
using PathCreation;

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
    public class PathCreatorContainer
    {
        public string wayPointName;
        public PathCreator[] pathCreators;
    }

    public class ReadOnlyAttribute : PropertyAttribute
    {

    }
}
