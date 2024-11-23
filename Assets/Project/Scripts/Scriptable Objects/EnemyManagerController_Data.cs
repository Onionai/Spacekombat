using UnityEngine;
using UnityEngine.Pool;

namespace Onion_AI
{
    [CreateAssetMenu(fileName = "Enemy Controller_Data", menuName = "OnionAI/Enemy Controller Data")]
    public class EnemyManagerController_Data : ScriptableObject
    {
        [Header("Enemy Manager Controller")]
        public ObjectPool<EnemyManagersController> enemyManagerControllerPool;
        [SerializeField] private EnemyManagersController enemyManagersController;

        public void Initialize(GameManager GM)
        {
            enemyManagersController.enemyManagerController_Data = this;
            enemyManagerControllerPool = ObjectSpawner.PoolEnemyControllers(GM, enemyManagersController);
        }
    }
}
