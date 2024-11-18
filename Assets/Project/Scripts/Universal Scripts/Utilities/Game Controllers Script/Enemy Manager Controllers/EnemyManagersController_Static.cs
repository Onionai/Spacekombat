using UnityEngine;

namespace Onion_AI
{
    public class EnemyManagersController_Static : EnemyManagersController
    {
        protected bool ShouldIgnore()
        {
            int random = Random.Range(1, 10);
            if(random >= 2)
            {
                return true;
            }
            return false;
        }

        protected SpawnPoint IgnoreSpawnPoint()
        {
            bool shouldIgnore = ShouldIgnore();
            if(shouldIgnore)
            {
                int random = Random.Range(0, spawnPoints.Count);
                return spawnPoints[random];
            }
            return null;
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
            SpawnPoint spawnPointToIgnore = IgnoreSpawnPoint();

            for(int i = 0; i < spawnPoints.Count; i++)
            {
                SpawnPoint spawnPoint = spawnPoints[i];

                if(spawnPoint == spawnPointToIgnore)
                {
                    continue;
                }
                spawnPoint.SpawnEnemyManager();
            }
        }
    }
}
