using System.Collections.Generic;
using UnityEngine;

namespace Onion_AI
{
    [CreateAssetMenu(fileName = "WayPointConfig", menuName = "OnionAI/WaypointConfig")]
    public class WayPointConfig : ScriptableObject
    {
        private int randomPoint;

        [field: Header("Controller Information")]
        private float movementSpeed;
        [SerializeField] private PathController pathController;
        [field: SerializeField] public PathControllerType pathControllerType {get; private set;} = PathControllerType.FourWayPath;

        [field: Header("WayPoint Information")]
        [field: SerializeField] public List<Transform> pathWay01 {get; private set;} = new();
        [field: SerializeField] public List<Transform> pathWay02 {get; private set;} = new();
        [field: SerializeField] public List<Transform> pathWay03 {get; private set;} = new();
        [field: SerializeField] public List<Transform> pathWayExit {get; private set;} = new();

        public void Initialize()
        {
            pathWay01 = GetWayPoints(pathController.wayPoints01Nodes);
            pathWay02 = GetWayPoints(pathController.wayPoints02Nodes);
            pathWay03 = GetWayPoints(pathController.wayPoints03Nodes);
            pathWayExit = GetWayPoints(pathController.exitWayPointsNodes);
        }

        public Transform GetNextWayPoint(List<Transform> pathWayList)
        {
            randomPoint = Random.Range(0, pathWayList.Count);
            return pathWayList[randomPoint];
        }

        public List<Transform> GetWayPoints(Transform[] pathWayArray)
        {
            List<Transform> wayPoints = new();
            foreach(Transform child in pathWayArray)
            {
                wayPoints.Add(child);
            }
            return wayPoints;
        }

        public float GetMovementSpeed()
        {
            return movementSpeed;
        }
    }
}
