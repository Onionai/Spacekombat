using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathCreation;

namespace Onion_AI
{
    public class SpawnPoint : MonoBehaviour
    {
        //Private Parameters
        private WaitForSeconds waitForSeconds;
        private Coroutine enemyShootingRoutine;
        private EnemyManager selectedCharacter;
        private List<int> spawnedEnemiesIndexes = new List<int>();

        [Header("Spawn Parameters")]
        public PathCreator pathCreator;
        [SerializeField] public Spawnable_Items spawnableItem;

        [Header("Enemy Parameters")]
        public List<EnemyManager> spawnedEnemies = new List<EnemyManager>();
        [SerializeField] private EnemyManagersController enemyManagersController;

        private void Awake()
        {
            waitForSeconds = new WaitForSeconds(3.0f);
            enemyManagersController = GetComponentInParent<EnemyManagersController>();
        }

        private void Start()
        {
            if(enemyManagersController.enemyType == EnemyType.Static)
            {
                return;
            }
        }

        public void Initialize(PathCreator pc)
        {
            spawnableItem = Instantiate(spawnableItem);

            pathCreator = pc;
            spawnableItem.enemyManagersController = enemyManagersController;
            spawnableItem.Initialize();
        }

        public void RandomShooting()
        {
            if(enemyShootingRoutine == null)
            {
                enemyShootingRoutine = StartCoroutine(EnemyAttackingStrategyCoroutine(null));
            }
        }

        public IEnumerator EnemyAttackingStrategyCoroutine(EnemyManager enemyManager)
        {
            if (enemyManagersController.missionStatus != MissionStatus.Active)
            {
                enemyShootingRoutine = null;
                enemyManagersController.StopCoroutine(EnemyAttackingStrategyCoroutine(null));
                yield break;
            }

            if(enemyManagersController.target == null)
            {
                enemyShootingRoutine = null;
                enemyManagersController.StopCoroutine(EnemyAttackingStrategyCoroutine(null));
                yield break;
            }

            yield return new WaitForSeconds(Random.Range(0.5f,1.5f));

            selectedCharacter = RandomEnemyExcluding(enemyManager);
            if (selectedCharacter == null)
            {
                enemyShootingRoutine = null;
                enemyManagersController.StopCoroutine(EnemyAttackingStrategyCoroutine(null));
                yield break;
            }

            selectedCharacter.canShoot = true;
            yield return new WaitForSeconds(Random.Range(0.5f,1.5f));
            
            selectedCharacter.canShoot = false;
            yield return waitForSeconds;

            if(spawnedEnemies.Count > 0)
            {
                enemyShootingRoutine = enemyManagersController.StartCoroutine(EnemyAttackingStrategyCoroutine(selectedCharacter));
            }
        }

        private EnemyManager RandomEnemyExcluding(EnemyManager enemyManager)
        {
            spawnedEnemiesIndexes.Clear();
            foreach(EnemyManager member in spawnedEnemies)
            {
                if(member.hasReachedFormation != true)
                {
                    continue;
                }

                if(member != enemyManager)
                {
                    spawnedEnemiesIndexes.Add(spawnedEnemies.IndexOf(member));
                }
            }

            if(spawnedEnemiesIndexes.Count == 0)
            {
                return null;
            }
            
            int random = Random.Range(0, spawnedEnemiesIndexes.Count);
            EnemyManager returnValue = spawnedEnemies[spawnedEnemiesIndexes[random]];
            return returnValue;
        }

        public void SpawnEnemyManager()
        {
            Enemy_Data objectSpawner = spawnableItem as Enemy_Data;
            if(objectSpawner != null)
            {
                objectSpawner.IObjectSpawner_SpawnObject(this);
            }
        }
    }
}
