using UnityEngine;

namespace Onion_AI
{
    public class PathController : MonoBehaviour
    {
        [field: Header("Controller Information")]
        [field: SerializeField] public PathControllerType pathControllerType = PathControllerType.FourWayPath;

        [field: Header("Paths")]
        [field: Header("Path 01")]
        public Transform[] wayPoints01Nodes;

        [field: Header("Path 02")]
        public Transform[] wayPoints02Nodes;

        [field: Header("Path 03")]
        public Transform[] wayPoints03Nodes;

        [field: Header("Exit Path")]
        public Transform[] exitWayPointsNodes;

    }
}
