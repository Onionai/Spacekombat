using UnityEngine;
using System.Collections;

namespace Onion_AI
{
    public class EnemySpawner : MonoBehaviour
    {
        private WaitForSeconds waitForSeconds;
        private int maxAmountOfEnemiesPerWave;
        private int currentAmountOfEnemiesThisWave;

        [Header("Parameters")]
        public int waveCount;
        public Transform spawnPoint;
        public BossManager bossManager;

        [Header("Spawn Controllers")]
        [SerializeField] private EnemyController[] enemyControllers;
        [ReadOnly] public EnemyController currentEnemyManagersController;

        private void Awake()
        {
            waitForSeconds = new WaitForSeconds(3.0f);
        }

        private void Start()
        {
            GameplayController controller = GameManager.Instance.Controller;

            controller.AddStateListener(HandleSpawnProcess);
            if(currentEnemyManagersController == null)
            {
                controller.SwitchGameState(GamePlayState.SpawningEnemy);
            }
        }

        private bool CanSpawnBoss()
        {
            if(waveCount > 0 && waveCount % 10 == 0)
            {
                return true;
            }
            return false;
        }

        private void HandleSpawnProcess(GamePlayState gamePlayState)
        {
            if (gamePlayState == GamePlayState.SpawningEnemy)
            {
                StartCoroutine(SpawnProcess());
            }
        }

        private IEnumerator SpawnProcess()
        {
            GameManager gameManager = GameManager.Instance;
            UIManager uiManager = gameManager.uiManager;

            bool canSpawnBoss = CanSpawnBoss();
            if (canSpawnBoss != true && currentAmountOfEnemiesThisWave >= maxAmountOfEnemiesPerWave)
            {
                CreateNewWave(uiManager);
            }
            yield return waitForSeconds;
            uiManager.PauseButton.interactable = true;
            uiManager.CenterTextUI.gameObject.SetActive(false);
            SpawnEnemyControllerOrBoss(canSpawnBoss, gameManager);
        }

        private void SpawnEnemyControllerOrBoss(bool canSpawnBoss, GameManager gameManager)
        {
            GameObject boss = bossManager.gameObject;
            if (canSpawnBoss)
            {
                boss.SetActive(true);
                bossManager.IncreaseAppearanceCount();
            }
            else
            {
                currentEnemyManagersController = CreateNewController();
                currentEnemyManagersController.StartSpawningEnemy();
            }
            gameManager.Controller.SwitchGameState(GamePlayState.Active);
        }

        private void CreateNewWave(UIManager uiManager)
        {
            waveCount++;
            currentAmountOfEnemiesThisWave = 0;
            maxAmountOfEnemiesPerWave = Random.Range(45, 65);
            uiManager.SetRoundCount(waveCount);
        }

        private EnemyController CreateNewController()
        {
            int random = Random.Range(0, enemyControllers.Length);
            EnemyController newController = Instantiate(enemyControllers[random], spawnPoint);
            newController.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            newController.Init(this);
            GameManager.Instance.playerManager.canShoot = true;
            currentAmountOfEnemiesThisWave += newController.MaxKillCount;
            return newController;
        }
    }
}
