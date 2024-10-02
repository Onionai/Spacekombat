using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onion_AI
{
    public class EnemyManagersController_Horizontal : EnemyManagersController
    {
        protected override void Spawn()
        {
            for(int i = 0; i < spawnPoints.Count; i++)
            {
                SpawnPoint spawnPoint = spawnPoints[i];
                spawnPoint.SpawnEnemyManager(0.0f, 0.0f);
            }
        }
    }
}
