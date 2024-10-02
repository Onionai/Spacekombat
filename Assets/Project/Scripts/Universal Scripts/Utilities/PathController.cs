using UnityEngine;

namespace Onion_AI
{
    public class PathController : MonoBehaviour
    {
        [field: Header("Controller Information")]
        [field: SerializeField] public EnemyType enemyTypePath = EnemyType.Static;

        [field: Header("Paths")]
        [field: Header("Path 01")]
        public Transform[] wayPoints01Nodes;

        [field: Header("Path 02")]
        public Transform[] wayPoints02Nodes;

        [field: Header("Path 03")]
        public Transform[] wayPoints03Nodes;

        [field: Header("Path 04")]
        public Transform[] wayPoints04Nodes;

        [field: Header("Pathways")]
        public Transform[] allWayPointsNodes;

    }
}
