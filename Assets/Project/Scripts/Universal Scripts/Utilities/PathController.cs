using UnityEngine;
using PathCreation;
using System.Collections.Generic;

namespace Onion_AI
{
    public class PathController : MonoBehaviour
    {
        [Header("Controller Information")]
        [SerializeField] private List<PathCreator> pathCreatorList = new();
        [SerializeField] private PathCreatorContainer[] pathCreatorContainers;

        public PathCreator RandomPathCreator_FreeRoam()
        {
            int random = Random.Range(0, pathCreatorList.Count);
            return pathCreatorList[random];
        }

        public PathCreatorContainer RandomPathCreatorClass()
        {
            int random = Random.Range(0, pathCreatorContainers.Length);
            return pathCreatorContainers[random];
        }
    }
}
