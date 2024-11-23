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

        public override void Initialize()
        {
            base.Initialize();

            EnemyManager randomObject = RandomObject();
            enemyPool = ObjectSpawner.PoolEnemyManager(randomObject);
        }

        public void IObjectSpawner_SpawnObject(SpawnPoint spawnPoint)
        {
            if(enemyPool == null)
            {
                return;
            }

            hasResetPosition = true;
            EnemyManager enemyManager = enemyPool.Get();
            enemyManager.transform.SetParent(spawnPoint.transform);
            enemyManager.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            
            enemyManager.Initialize(this, enemyManagersController, spawnPoint);
            enemyManagersController.StartCoroutine(SetEnemyManagerProperties(enemyManager));
        }

        private IEnumerator SetEnemyManagerProperties(EnemyManager enemyManager)
        {
            yield return new WaitUntil(() => hasResetPosition);
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
