using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Onion_AI
{
    [CreateAssetMenu(fileName = "Enemy_Data", menuName = "OnionAI/SpawnItems/EnemyData")]
    public class Enemy_Data : Spawnable_Items, IObjectSpawner
    {
        [field: Header("Enemy Information")]
        private bool hasResetPosition;
        public string spawnName {get; private set;}
        [SerializeField] private EnemyManager[] enemyManagers;
        public ObjectPool<EnemyManager> enemyPool {get; private set;}

        public override void Initialize(int quantity, int maxQuantity, Transform spawnPoint)
        {
            objectSpawner = this;
            EnemyManager randomObject = RandomObject();

            base.Initialize(quantity, maxQuantity, spawnPoint);
            enemyPool = ObjectSpawner.PoolEnemyManager(quantity, maxQuantity, spawnPoint, randomObject);
        }

        public void IObjectSpawner_SpawnObject(float minWidth, float maxWidth)
        {
            EnemyManager enemyManager = enemyPool.Get();

            enemyManager.enemyData = this;
            enemyManager.Initialize(enemySpawner.gameManager);
            float randomPositionX = Random.Range(minWidth, maxWidth);

            Vector2 spawnPosition = new Vector2(randomPositionX, 0.0f);
            enemyManager.transform.SetLocalPositionAndRotation(spawnPosition, Quaternion.identity);
            hasResetPosition = true;
            
            enemySpawner.StartCoroutine(SetEnemyManagerProperties(enemyManager));
        }

        private IEnumerator SetEnemyManagerProperties(EnemyManager enemyManager)
        {
            yield return new WaitUntil(() => hasResetPosition);
            enemyManager.SetEnemyFirstPath(false);

            yield return new WaitUntil(() => enemyManager.hasSetPath == false);
            hasResetPosition = false;
            enemyManager.gameObject.SetActive(true);
            enemySpawner.StopCoroutine(SetEnemyManagerProperties(enemyManager));
        }

        private EnemyManager RandomObject()
        {
            int selectedOption = Random.Range(0,enemyManagers.Length);
            EnemyManager selectedObject = enemyManagers[selectedOption];
            return selectedObject;
        }
    }
}
