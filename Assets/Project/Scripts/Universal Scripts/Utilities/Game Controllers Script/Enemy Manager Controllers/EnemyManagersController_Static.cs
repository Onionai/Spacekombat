using UnityEngine;
using System.Collections.Generic;

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
            SpawnPoint spawnPointToIgnore = IgnoreSpawnPoint();

            for(int i = 0; i < spawnPoints.Count; i++)
            {
                SpawnPoint spawnPoint = spawnPoints[i];

                if(spawnPoint == spawnPointToIgnore)
                {
                    continue;
                }
                spawnPoint.SpawnEnemyManager(0.0f, 0.0f);
            }
        }
    }
}
