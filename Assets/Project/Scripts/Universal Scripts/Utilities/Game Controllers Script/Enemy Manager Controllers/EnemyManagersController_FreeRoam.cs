using UnityEngine;

namespace Onion_AI
{
    public class EnemyManagersController_FreeRoam : EnemyManagersController
    {
        [field: Header("Personal Parameters")]
        [SerializeField] private float maximumWidth;
        [SerializeField] private float minimumWidth;

        protected override void SpawnEnemies()
        {
            if(Time.time <= nextSpawnTime)
            {
                return;
            }

            nextSpawnTime = Time.time + spawnRate;
            Spawn();
        }

        protected override void Spawn()
        {
            for(int i = 0; i < spawnPoints.Count; i++)
            {
                SpawnPoint spawnPoint = spawnPoints[i];
                spawnPoint.SpawnEnemyManager(minimumWidth, maximumWidth);
            }
        }

        public override void InitializeMovement(EnemyManager enemyManager)
        {
            EnemyMovement enemyMovement = enemyManager.enemyMovement;

            enemyMovement.currentPathIndex = 0;
            enemyMovement.currentWayPoint = enemyManager.spawnPoint.GetNextWayPoint(enemyMovement.currentPathIndex);
        }
        //Functionalities
    }
}
