using UnityEngine;
using System.Collections;

namespace Onion_AI
{
    public class EnemySpawner : MonoBehaviour
    {
        private enum SpawnStatus
        {
            ShowWaveCount,
            CanSpawn,
            Finished
        }

        private int numberOfSpawns;
        public static int waveCount;
        private WaitForSeconds waitForSeconds;

        [Header("Boss System")]
        public bool canSpawnBoss;
        public BossManager bossManager;

        [Header("Components")]
        public GameManager gameManager;
        
        [field: Header("Parameters")]
        public Transform spawnPoint;
        private SpawnStatus spawnStatus;
        [field: SerializeField] public int spawnQuantity {get; protected set;}

        [Header("Spawn Controllers")]
        public EnemyManagersController currentEnemyManagersController;
        private EnemyManagersController previousEnemyManagersController;

        [SerializeField]
        private EnemyManagerController_Data[] enemyManagersControllerData;

        protected virtual void Awake()
        {
            waveCount = 1;
            spawnQuantity = Random.Range(7,11);
            spawnStatus = SpawnStatus.ShowWaveCount;
        }

        private void Start()
        {
            waitForSeconds = new WaitForSeconds(3.0f);

            for (int i = 0; i < enemyManagersControllerData.Length; i++)
            {
                EnemyManagerController_Data controller_Data = Instantiate(enemyManagersControllerData[i]);

                enemyManagersControllerData[i] = controller_Data;
                enemyManagersControllerData[i].Initialize(gameManager);
            }
        }

        public virtual void EnemySpawn_Updater()
        {
            canSpawnBoss = CanSpawnBoss();

            SpawnObject();
            if (currentEnemyManagersController != null)
            {
                currentEnemyManagersController.EnemyManagerController_Updater();
            }
            
        }

        private bool CanSpawnBoss()
        {
            if(waveCount == 30 || waveCount == 60 || waveCount == 100)
            {
                return true;
            }
            return false;
        }

        protected void SpawnObject()
        {
            if(gameManager.playerManager.isDead)
            {
                return;
            }

            if(currentEnemyManagersController?.missionStatus == MissionStatus.Active)
            {
                return;
            }

            if(currentEnemyManagersController?.missionStatus == MissionStatus.Failed)
            {
                previousEnemyManagersController = currentEnemyManagersController;
                if (previousEnemyManagersController != null && previousEnemyManagersController.gameObject.activeSelf == true)
                {
                    previousEnemyManagersController.ReleaseObject();
                }

                if (canSpawnBoss) // And Boss Not Dead
                {
                    if (bossManager.gameObject.activeSelf != true)
                    {
                        bossManager.gameObject.SetActive(true);
                        bossManager.IncreaseAppearanceCount();
                    }
                }
                currentEnemyManagersController = null;
                spawnStatus = SpawnStatus.ShowWaveCount;
            }

            if(canSpawnBoss && bossManager.isDead != true)
            {
                return;
            }

            if(spawnStatus == SpawnStatus.ShowWaveCount)
            {
                gameManager.uIManager.PauseButton.interactable = false;
                gameManager.uIManager.SetRoundCount(waveCount);
                
                if(waveCount > 1) 
                {
                    gameManager.playerManager.PlayReloadAnimation();
                }
                StartCoroutine(DisableRoundCountAndSpawn());
            }

            if(spawnStatus == SpawnStatus.CanSpawn)
            {
                Spawn();
            }
        }

        private IEnumerator DisableRoundCountAndSpawn()
        {
            yield return waitForSeconds;
            spawnStatus = SpawnStatus.CanSpawn;
            gameManager.uIManager.CenterRoundCountUI.gameObject.SetActive(false);
            gameManager.uIManager.PauseButton.interactable = true;
        }

        protected void Spawn()
        {
            if(currentEnemyManagersController != null)
            {
                return;
            }
            
            waveCount++;
            spawnStatus = SpawnStatus.Finished;

            int random = Random.Range(0, enemyManagersControllerData.Length);
            EnemyManagerController_Data randomData = enemyManagersControllerData[random];

            currentEnemyManagersController = randomData.enemyManagerControllerPool.Get();
            currentEnemyManagersController.transform.parent = spawnPoint;
            currentEnemyManagersController.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            
            gameManager.playerManager.canShoot = true;
        }
    }
}
