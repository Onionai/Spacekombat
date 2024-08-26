using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onion_AI
{
    public class Spawnable_Items : ScriptableObject
    {
        public EnemySpawner enemySpawner;
        public IObjectSpawner objectSpawner;
        public virtual void Initialize(int quantity, int maxQuantity, Transform spawnPoint)
        {

        }
    }
}
