using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    public class EnemyMovement : CharacterMovement
    {
        [Header("Parameters")]
        public float distanceToTarget;
        public float maxDistanceToTarget;
        private EnemyManager enemyManager;

        [Header("Path Information")]
        public int currentPathIndex;
        public Transform currentWayPoint;
        private Vector3 wayPointPosition;
        public List<List<Transform>> pathWayList = new();

        protected override void Awake()
        {
            base.Awake();
            enemyManager = characterManager as EnemyManager;
        }

        protected override void OnEnable()
        {
            if(enemyManager.hasSetPath != true)
            {
                GetPathWayList();

                currentPathIndex = 0;
                currentWayPoint = enemyManager.wayPointConfig.GetNextWayPoint(pathWayList[currentPathIndex]);
                wayPointPosition = currentWayPoint.position;
                enemyManager.SetEnemyFirstPath(true);
            }
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void CharacterMovement_FixedUpdate(float delta)
        {
            HandleMovement(delta);
            base.CharacterMovement_FixedUpdate(delta);
        }

        //Functionalities 
        
        protected override void HandleMovement(float delta)
        {
            if(distanceToTarget <= maxDistanceToTarget)
            {
                currentPathIndex++;
                if(currentPathIndex >= pathWayList.Count)
                {
                    pathWayList.Clear();
                    enemyManager.enemyData.enemyPool.Release(enemyManager);
                    return;
                }
                currentWayPoint = enemyManager.wayPointConfig.GetNextWayPoint(pathWayList[currentPathIndex]);
                wayPointPosition = currentWayPoint.position;
            }
            float speed = acceleration * movementSpeed * delta;
            distanceToTarget = Vector3.Distance(enemyManager.transform.position, wayPointPosition);
            enemyManager.transform.position = Vector3.MoveTowards(enemyManager.transform.position, wayPointPosition, speed);
        }

        protected override void HandleRotation(float delta)
        {

        }

        public void GetPathWayList()
        {
            enemyManager.wayPointConfig.Initialize();
            pathWayList.Add(enemyManager.wayPointConfig.pathWay01);
            pathWayList.Add(enemyManager.wayPointConfig.pathWay02);
            pathWayList.Add(enemyManager.wayPointConfig.pathWay03);
            pathWayList.Add(enemyManager.wayPointConfig.pathWayExit);
        }
    }
}
