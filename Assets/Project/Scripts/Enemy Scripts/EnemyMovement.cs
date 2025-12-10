using UnityEngine;
using PathCreation;

namespace Onion_AI
{
    public class EnemyMovement : CharacterMovement
    {
        private EnemyManager enemyManager;

        private float disRemaining;

        [Header("Enemy Behavioral Movement")]
        [ReadOnly] public int index;
        [ReadOnly] public bool constrainMovement;

        protected override void Awake()
        {
            base.Awake();
            enemyManager = characterManager as EnemyManager;
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
            if (enemyManager.hasReachedTarget)
            {
                return;
            }
            float waveSpeedMultiplier = (GameManager.Instance.EnemySpawn.waveCount) + 1 / 100;
            float speed = movementSpeed * delta * waveSpeedMultiplier;
            if(enemyManager.enemyType.Equals(EnemyType.FreeFall) != true)
            {
                HandleNonFreeFallMovement(speed);                
                return;
            }
            enemyManager.transform.position += Vector3.down * speed;
        }

        private void HandleNonFreeFallMovement(float speed)
        {
            disRemaining += speed;
            VertexPath path = enemyManager.PathCreatorClass.path;

            float pathLength = path.length;
            if(enemyManager.enemyType.Equals(EnemyType.Formation) && disRemaining >= pathLength)
            {
                EnemyController_FormationBased formation = enemyManager.Controller as EnemyController_FormationBased;

                index = formation.spawnedEnemies.IndexOf(enemyManager);
                Vector3 spawnHolderPosition = formation.SpawnHolderPosition();

                Vector3 targetPosition = spawnHolderPosition + formation.FormationPoints[index];
                float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
                enemyManager.hasReachedTarget = distanceToTarget <= 0.25f;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
                return;
            }
            transform.position = path.GetPointAtDistance(disRemaining, EndOfPathInstruction.Stop);
        }
        
        public void ResetDistanceRemaining()
        {
            disRemaining = 0.0f;
        }
    }
}
