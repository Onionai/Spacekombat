using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

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

        public override void Initialize(int quantity, int maxQuantity)
        {
            base.Initialize(quantity, maxQuantity);

            EnemyManager randomObject = RandomObject();
            enemyPool = ObjectSpawner.PoolEnemyManager(quantity, maxQuantity, randomObject);
        }

        public void IObjectSpawner_SpawnObject(float minWidth, float maxWidth, SpawnPoint spawnPoint)
        {
            if(enemyPool == null)
            {
                return;
            }
            EnemyManager enemyManager = enemyPool.Get();

            float randomPositionX = Random.Range(minWidth, maxWidth);
            Vector2 spawnPosition = new Vector2(randomPositionX, 0.0f);

            hasResetPosition = true;
            enemyManager.transform.SetParent(spawnPoint.transform);
            enemyManager.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
            enemyManager.transform.SetLocalPositionAndRotation(spawnPosition, Quaternion.identity);
            
            spawnPoint.spawnedEnemies.Add(enemyManager);
            enemyManager.Initialize(this, enemyManagersController, spawnPoint);
            enemyManagersController.StartCoroutine(SetEnemyManagerProperties(enemyManager));
        }

        private IEnumerator SetEnemyManagerProperties(EnemyManager enemyManager)
        {
            yield return new WaitUntil(() => hasResetPosition);
            enemyManager.SetEnemyFirstPath(false);

            yield return new WaitUntil(() => enemyManager.hasSetPath == false);
            hasResetPosition = false;
            enemyManager.gameObject.SetActive(true);
            
            enemyManagersController.numberOfSpawns++;
            enemyManagersController.StopCoroutine(SetEnemyManagerProperties(enemyManager));
        }

        private EnemyManager RandomObject()
        {
            int selectedOption = Random.Range(0,enemyManagers.Length);
            EnemyManager selectedObject = enemyManagers[selectedOption];
            return selectedObject;
        }
    }
}
