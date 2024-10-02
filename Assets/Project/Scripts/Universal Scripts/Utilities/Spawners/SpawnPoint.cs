using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        [SerializeField] public Spawnable_Items spawnableItem;

        [Header("Enemy Parameters")]
        public List<EnemyManager> spawnedEnemies = new List<EnemyManager>();
        [SerializeField] private EnemyManagersController enemyManagersController;

        [field: Header("WayPoint and Formation Configuration")]
        public List<Transform> singlePathwayList = new List<Transform>();
        public List<List<Transform>> pathWayList = new List<List<Transform>>();
        [field: SerializeField] public WayPointConfig wayPointConfig {get; private set;}

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
            wayPointConfig = Instantiate(wayPointConfig);
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
                if(member != enemyManager)
                {
                    spawnedEnemiesIndexes.Add(spawnedEnemies.IndexOf(member));
                }
            }

            int random = Random.Range(0, spawnedEnemiesIndexes.Count);
            EnemyManager returnValue = spawnedEnemies[spawnedEnemiesIndexes[random]];
            return returnValue;
        }

        public void Initialize(int numberOfSpawns)
        {
            spawnableItem = Instantiate(spawnableItem);
            spawnableItem.enemyManagersController = enemyManagersController;
            spawnableItem.Initialize(numberOfSpawns, numberOfSpawns + 10);
        }

        public void GetPathWayList()
        {
            if(enemyManagersController.enemyType == EnemyType.Static)
            {
                return;
            }
            pathWayList.Clear();
            singlePathwayList.Clear();

            wayPointConfig.Initialize();
            pathWayList.Add(wayPointConfig.pathWay01);
            pathWayList.Add(wayPointConfig.pathWay02);
            pathWayList.Add(wayPointConfig.pathWay03);
            pathWayList.Add(wayPointConfig.pathWay04);

            for(int i = 0; i < wayPointConfig.totalPathWays.Count; i++)
            {
                Transform pathway = wayPointConfig.totalPathWays[i];
                singlePathwayList.Add(pathway);
            }
        }

        public Vector3 GetNextPoint(int currentPathIndex)
        {
            return singlePathwayList[currentPathIndex].position;
        }

        public Transform GetNextWayPoint(int currentPathIndex)
        {
            List<Transform> wayPoint = new (pathWayList[currentPathIndex]);

            int randomPoint = Random.Range(0, wayPoint.Count);
            return wayPoint[randomPoint];
        }

        public void SpawnEnemyManager(float minWidth, float maxWidth)
        {
            Enemy_Data objectSpawner = spawnableItem as Enemy_Data;
            if(objectSpawner != null)
            {
                objectSpawner.IObjectSpawner_SpawnObject(minWidth, maxWidth, this);
            }
        }
    }
}
