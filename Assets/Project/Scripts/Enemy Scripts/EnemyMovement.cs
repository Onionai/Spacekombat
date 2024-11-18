using PathCreation;
using UnityEngine;

namespace Onion_AI
{
    public class EnemyMovement : CharacterMovement
    {
        private float speed;
        private PathCreator pathCreator;
        private EnemyManager enemyManager;

        //EnemyManagers Controller
        EnemyManagersController enemyManagersController;
        EnemyManagersController_Fixed enemyManagersController_Fixed;

        private float pathLength;
        private float distanceRemaining;

        protected override void Awake()
        {
            base.Awake();
            enemyManager = characterManager as EnemyManager;
        }

        public void Initialize()
        {
            SpawnPoint spawnPoint = enemyManager.spawnPoint;
            pathCreator = spawnPoint.pathCreator;
            enemyManagersController = enemyManager.enemyManagersController;

            if(enemyManager.enemyType == EnemyType.Linear)
            {
                enemyManagersController_Fixed = enemyManagersController as EnemyManagersController_Fixed;
            }
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void CharacterMovement_FixedUpdate(float delta)
        {
            if(enemyManager.attemptSuicide)
            {
                return;
            }
            
            HandleMovement(delta);
            base.CharacterMovement_FixedUpdate(delta);
        }

        //Functionalities 
        
        protected override void HandleMovement(float delta)
        {
            float waveSpeedMultiplier = (EnemySpawner.waveCount - 1) / 10f;
            speed = acceleration * movementSpeed * delta * waveSpeedMultiplier;

            if(enemyManager.enemyType == EnemyType.Linear)
            {
                HandleLinearMovement();
            }
            else if(enemyManager.enemyType == EnemyType.Static)
            {
                HandleStaticMovement();
            }
            else if(enemyManager.enemyType == EnemyType.FreeRoam)
            {
                HandleFreeRoamEnemyMovement();
            }
        }

        private void HandleLinearMovement()
        {
            distanceRemaining += speed;
            pathLength = pathCreator.path.length;

            if(distanceRemaining >= pathLength)
            {
                int index = enemyManagersController.spawnedEnemies.IndexOf(enemyManager);

                Vector3 spawnHolderPosition = enemyManagersController_Fixed.SpawnHolderPosition();
                Vector3 targetPosition = spawnHolderPosition + enemyManagersController_Fixed.formationPoints[index];

                float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
                enemyManager.hasReachedFormation = distanceToTarget <= 0.25f;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
                return;
            }
            transform.position = pathCreator.path.GetPointAtDistance(distanceRemaining, EndOfPathInstruction.Stop);
        }

        private void HandleStaticMovement()
        {
            enemyManager.transform.position += Vector3.down * speed;
        }

        private void HandleFreeRoamEnemyMovement()
        {
            enemyManager.canShoot = true;
            pathCreator = enemyManager.pathCreator;
            
            distanceRemaining += speed;
            transform.position = pathCreator.path.GetPointAtDistance(distanceRemaining, EndOfPathInstruction.Stop);
        }

        public void ResetDistanceRemaining()
        {
            distanceRemaining = 0.0f;
        }
    }
}
