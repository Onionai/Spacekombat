using UnityEngine;

namespace Onion_AI
{
    public class EnemyManagersController_FreeRoam : EnemyManagersController
    {
        protected override void SpawnEnemies()
        {
            if(Time.time <= nextSpawnTime)
            {
                return;
            }

            nextSpawnTime = Time.time + spawnRate;
            Spawn();
        }

        public override void EnemyManagerController_Updater()
        {
            if(target.isDead)
            {
                missionStatus = MissionStatus.Completed;
                gameObject.SetActive(false);
                return;
            }
            
            if(hasSetSpawnQuantity && killedEnemies >= spawnQuantity)
            {
                missionStatus = MissionStatus.Failed;
                return;
            }

            SpawnEnemies();
        }

        protected override void PrepareSpawnPoints()
        {
            for(int i = 0; i < spawnPoints.Count; i++)
            {
                SpawnPoint spawnPoint = spawnPoints[i];
                 
                spawnPoint.spawnedEnemies.Clear();
                spawnPoint.Initialize(null);
            }
        }


        protected override void Spawn()
        {
            for(int i = 0; i < spawnPoints.Count; i++)
            {
                SpawnPoint spawnPoint = spawnPoints[i];
                spawnPoint.SpawnEnemyManager();
            }
        }
        //Functionalities
    }
}
