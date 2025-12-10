using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Onion_AI
{
    public class EnemyController_FormationBased : EnemyController
    {
        private int maxSpawnRounds;
        private int currentSpawnRound;
        private WaitForSeconds waitForSeconds;

        [Header("Type Based Parameters")]
        [SerializeField] private FormationController formationController;
        [field: ReadOnly] public List<Vector3> FormationPoints { get; private set; }

        public override void Init(EnemySpawner enemySpawner)
        {
            currentSpawnRound = 0;
            base.Init(enemySpawner);
            MaxKillCount = KillCount();
            waitForSeconds = new WaitForSeconds(1.75f);

            FormationPoints = new();
            maxSpawnRounds = Random.Range(2, 4);
            maxConcurrentEnemies = MaxKillCount/maxSpawnRounds;
        }

        protected override IEnumerator SpawnRoutine()
        {
            spawnedEnemies.Clear();
            FormationPoints.Clear();
            float elapsedSeconds = 0f;
            GameManager.Instance.playerManager.canShoot = false;

            UIManager uiManager = GameManager.Instance.uiManager;
            uiManager.SetCenterText($"Wave: {enemySpawner.waveCount} Round: {currentSpawnRound}");
            yield return waitForSeconds;

            uiManager.PauseButton.interactable = true;
            uiManager.CenterTextUI.gameObject.SetActive(false);

            GameManager.Instance.playerManager.canShoot = true;
            bool divisibleBySix = (maxConcurrentEnemies % 6 == 0);
            formationController.Initialize(divisibleBySix, maxConcurrentEnemies);

            int count = 0;
            FormationPoints = formationController.EvaluatePoints().ToList();
            PathCreatorContainer controllerClass = Path.RandomPathCreatorClass();
            while (count < maxConcurrentEnemies)
            {
                float interval;
                float x = (spawnDecayMode == SpawnDecayMode.ByTimeSeconds) ? elapsedSeconds : (float)currentKillCount;
                if (!useRateMode)
                {
                    interval = spawnRate * Mathf.Pow(intervalDecayFactor, x);
                    interval = Mathf.Max(minInterval, interval);
                }
                else
                {
                    float rate = baseRate * Mathf.Pow(rateGrowthFactor, x);
                    interval = 1f / Mathf.Max(1e-4f, rate);
                    interval = Mathf.Max(minInterval, interval);
                }
                int spawnPointIdx = Random.Range(0, spawnPoints.Length);
                SpawnOne_Pooled(spawnPointIdx, controllerClass.pathCreators[spawnPointIdx]);

                yield return null;
                elapsedSeconds += interval;
                yield return new WaitForSecondsRealtime(interval);
                count++;
            }
            spawnRoutine = null;
            yield break;
        }

        public override void StartSpawningEnemy()
        {
            if(currentKillCount >= MaxKillCount && currentSpawnRound >= maxSpawnRounds)
            {
                EnemyMissionFailed();
                return;
            }

            if(spawnedEnemies.Count != 0)
            {
                return;
            }
            currentSpawnRound++;
            spawnRoutine = StartCoroutine(SpawnRoutine());
        }

        public Vector3 SpawnHolderPosition()
        {
            if (formationController.formationType == FormationType.Box)
            {
                return transform.position;
            }
            return new Vector3(-0.4f, transform.position.y + 0.2f, 0);
        }

        private int KillCount()
        {
            if(Random.Range(0,6) <= 2)
            {
                return Count(36, 42, 48);
            }
            return Count(32, 34, 36);
        }

        private int Count(int min, int med, int max)
        {
            int rnd = Random.Range(0, 9);
            if(rnd % 3 == 0)
            {
                return min;
            }
            else if(rnd % 3 == 1)
            {
                return med;
            }
            return max;
        }
    }
}
