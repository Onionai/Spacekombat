using UnityEngine;

namespace Onion_AI
{
    public class Spawnable_Items : ScriptableObject
    {
        public EnemyManagersController enemyManagersController;
        public virtual void Initialize(int quantity, int maxQuantity)
        {

        }
    }
}
