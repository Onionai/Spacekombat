using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    public class EnemyManagersController_Vertical : EnemyManagersController
    {
        protected override void Spawn()
        {
            for(int i = 0; i < spawnPoints.Count; i++)
            {
                SpawnPoint spawnPoint = spawnPoints[i];
                spawnPoint.SpawnEnemyManager(0.0f, 0.0f);
            }
        }

        public override void InitializeMovement(EnemyManager enemyManager)
        {
            
        }
    }
}
