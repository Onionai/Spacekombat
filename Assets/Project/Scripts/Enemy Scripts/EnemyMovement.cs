using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    public class EnemyMovement : CharacterMovement
    {
        private float interpolateAmount;

        [Header("Parameters")]
        public int cyclesLeft;
        public float distanceToTarget;
        public float maxDistanceToTarget;
        private EnemyManager enemyManager;

        [Header("Path Information")]
        public int currentPathIndex;
        public Transform currentWayPoint;
        private Vector3 wayPointPosition;

        protected override void Awake()
        {
            base.Awake();
            enemyManager = characterManager as EnemyManager;
        }

        public void Initialize()
        {
            if(enemyManager.hasSetPath == true)
            {
                return;
            }

            if(enemyManager.enemyType == EnemyType.FreeRoam)
            {
                enemyManager.enemyManagersController.InitializeMovement(enemyManager);
                wayPointPosition = currentWayPoint.position;
            }
            enemyManager.SetEnemyFirstPath(true);
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
            if(enemyManager.enemyType != EnemyType.Static && enemyManager.enemyType != EnemyType.FreeRoam)
            {
                HandleLinearMovement(delta);
            }
            else if(enemyManager.enemyType == EnemyType.Static)
            {
                HandleStaticMovement(delta);
            }
            else if(enemyManager.enemyType == EnemyType.FreeRoam)
            {
                HandleFreeRoamEnemyMovement(delta);
            }
        }

        private void HandleLinearMovement(float delta)
        {
            SpawnPoint spawnPoint = enemyManager.spawnPoint;

            if(enemyManager.enemyManagersController.hasBeenSet)
            {
                return;
            }
            interpolateAmount = (interpolateAmount + delta) % 1f;
            enemyManager.transform.position = MathsPhysicsTool.CubicLerp(transform.position, spawnPoint.GetNextPoint(1), spawnPoint.GetNextPoint(2), spawnPoint.GetNextPoint(3), interpolateAmount);
        }

        private void HandleStaticMovement(float delta)
        {
            float speed = acceleration * movementSpeed * delta;
            enemyManager.transform.position += Vector3.down * speed;
        }

        private void HandleFreeRoamEnemyMovement(float delta)
        {
            enemyManager.canShoot = true;
            distanceToTarget = Vector3.Distance(enemyManager.transform.position, wayPointPosition);

            if(distanceToTarget <= maxDistanceToTarget)
            {
                currentPathIndex++;
                if(currentPathIndex >= enemyManager.spawnPoint.pathWayList.Count)
                {
                    enemyManager.enemyData.enemyPool.Release(enemyManager);
                    return;
                }
                currentWayPoint = enemyManager.spawnPoint.GetNextWayPoint(currentPathIndex);
                wayPointPosition = currentWayPoint.position;
            }
            float speed = acceleration * movementSpeed * delta;
            enemyManager.transform.position = Vector3.MoveTowards(enemyManager.transform.position, wayPointPosition, speed);
        }
    }
}
