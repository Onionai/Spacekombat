using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

namespace Onion_AI
{
    [CreateAssetMenu(fileName = "Enemy_Data", menuName = "OnionAI/SpawnItems/EnemyData")]
    public class Enemy_Data : ScriptableObject
    {
        private bool hasResetPosition;
        private EnemyController enemyController;

        [Header("Enemy Information")]
        [SerializeField] private EnemyManager enemy;
        public ObjectPool<EnemyManager> EnemyPool {get; private set;}

        public void Initialize(EnemyController emc)
        {
            enemyController = emc;
            EnemyPool = ObjectSpawner.PoolEnemyManager(enemy);
        }

        public EnemyManager SpawnObject(Transform spawnPoint)
        {
            hasResetPosition = true;
            EnemyManager enemyManager = EnemyPool.Get();

            Transform transform = enemyManager.transform;
            transform.SetParent(spawnPoint.transform);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            enemyController.StartCoroutine(SetEnemyManagerProperties(enemyManager));
            return enemyManager;
        }

        private IEnumerator SetEnemyManagerProperties(EnemyManager enemyManager)
        {
            yield return new WaitUntil(HasResetPosition);

            hasResetPosition = false;
            enemyManager.gameObject.SetActive(true);
            //enemyController.StopCoroutine(SetEnemyManagerProperties(enemyManager));
        }

        private bool HasResetPosition()
        {
            return hasResetPosition;
        }
    }
}
