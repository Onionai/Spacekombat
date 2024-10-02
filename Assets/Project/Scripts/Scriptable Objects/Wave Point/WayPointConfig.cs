using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    [CreateAssetMenu(fileName = "WayPointConfig", menuName = "OnionAI/WaypointConfig")]
    public class WayPointConfig : ScriptableObject
    {
        [field: Header("Controller Information")]
        [SerializeField] private PathController pathController;

        [field: Header("WayPoint Information")]
        [field: SerializeField] public List<Transform> pathWay01 {get; private set;} = new();
        [field: SerializeField] public List<Transform> pathWay02 {get; private set;} = new();
        [field: SerializeField] public List<Transform> pathWay03 {get; private set;} = new();
        [field: SerializeField] public List<Transform> pathWay04 {get; private set;} = new();
        [field: SerializeField] public List<Transform> totalPathWays {get; private set;} = new();

        public void Initialize()
        {
            pathWay01 = GetWayPoints(pathController.wayPoints01Nodes);
            pathWay02 = GetWayPoints(pathController.wayPoints02Nodes);
            pathWay03 = GetWayPoints(pathController.wayPoints03Nodes);
            pathWay04 = GetWayPoints(pathController.wayPoints04Nodes);
            totalPathWays = GetWayPoints(pathController.allWayPointsNodes);
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
    }
}
