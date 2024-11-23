using UnityEngine;
using PathCreation;
using System.Collections.Generic;

namespace Onion_AI
{
    public class PathController : MonoBehaviour
    {
        [Header("Controller Information")]
        [SerializeField] private PathControllerClass[] pathControllerClassArray;
        [SerializeField] private List<PathCreator> pathCreatorList = new List<PathCreator>();

        public PathCreator RandomPathCreator_FreeRoam()
        {
            int random = Random.Range(0, pathCreatorList.Count);
            return pathCreatorList[random];
        }

        public PathControllerClass RandomPathCreator_Fixed()
        {
            int random = Random.Range(0, pathCreatorList.Count);
            return pathControllerClassArray[random];
        }
    }
}
