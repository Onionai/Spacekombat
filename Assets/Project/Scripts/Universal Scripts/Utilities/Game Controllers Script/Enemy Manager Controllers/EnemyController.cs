using UnityEngine;
using PathCreation;
using System.Collections;
using System.Collections.Generic;

namespace Onion_AI
{
    public class EnemyController : MonoBehaviour
    {
        public enum SpawnDecayMode
        {
            ByKills,
            ByTimeSeconds
        }
        protected Coroutine spawnRoutine;

        protected int currentKillCount;
        protected EnemySpawner enemySpawner;
        public int MaxKillCount { get; protected set; }
        [field: SerializeField] public PathController Path { get; protected set; }

        [Header("Parameters")]
        [SerializeField] protected EnemyType enemyType;
        [SerializeField] protected Enemy_Data[] enemyData;
        [ReadOnly] public List<EnemyManager> spawnedEnemies = new();

        [Header("Spawn Parameters")]
        [SerializeField] protected float spawnRate;
        [SerializeField] protected Transform[] spawnPoints;
        [SerializeField] protected SpawnDecayMode spawnDecayMode;

        [Header("FreeForm Spawn Tuning")]
        [Tooltip("When false: interval-decay mode (spawnRate is seconds-between-spawns).\n" +
                 "When true: rate-growth mode (baseRate is spawns-per-second).")]
        [SerializeField] protected bool useRateMode = false;
        [Tooltip("When useRateMode == true, this is the base spawns-per-second.")]
        [SerializeField] protected float baseRate = 0.5f;
        [Tooltip("When useRateMode == true, growth factor > 1 (e.g. 1.02) per unit (time or kill).")]
        [SerializeField] protected float rateGrowthFactor = 1.02f;
        [Tooltip("Min seconds between spawns (hard clamp).")]
        [SerializeField] protected float minInterval = 0.2f;
        [Tooltip("Interval decay factor in (0,1]. Default ~0.95 produces gentle acceleration.")]
        [Range(0.01f, 1f)][SerializeField] protected float intervalDecayFactor = 0.95f;
        [Tooltip("Maximum number of alive enemies allowed. 0 = unlimited")]
        [SerializeField] protected int maxConcurrentEnemies = 0;

        public EnemyType TypeOfEnemy => enemyType;

        public virtual void Init(EnemySpawner enemySpawner)
        {
            currentKillCount = 0;
            for(int i = 0; i < enemyData.Length; i++)
            {
                enemyData[i] = Instantiate(enemyData[i]);
                enemyData[i].Initialize(this);
            }
            this.enemySpawner = enemySpawner;
        }

        protected int SetKillCount(float multiplier)
        {
            float random = Random.Range(1.75f, 4.0f);
            return Mathf.CeilToInt(random * multiplier);
        }

        public virtual void StartSpawningEnemy()
        {
            if(currentKillCount >= MaxKillCount)
            {
                EnemyMissionFailed();
                return;
            }
            HandleSpawn();
        }

        protected void HandleSpawn()
        {
            if (spawnRoutine != null)
            {
                return;
            }
            spawnRoutine = StartCoroutine(SpawnRoutine());
        }

        public void KilledEnemy(Vector3 pos)
        {
            GameManager gameManager = GameManager.Instance;

            currentKillCount++;
            gameManager.playerManager.killCount++;
            if(gameManager.playerManager.killCount % 5 == 0)
            {
                gameManager.SpawnPowerUp(pos);
            }
        }

        protected void EnemyMissionFailed()
        {
            enemySpawner.currentEnemyManagersController = null;
            GameManager.Instance.Controller.SwitchGameState(GamePlayState.SpawningEnemy);
            Destroy(gameObject, 1.0f);
        }

        protected virtual IEnumerator SpawnRoutine()
        {
            yield return null;
        }

        protected void SpawnOne_Pooled(int spawnTransformIndex, PathCreator creator)
        {
            Transform spawnTransform = spawnPoints[spawnTransformIndex];
            var entry = enemyData.Length > 1 ? enemyData[Random.Range(0, enemyData.Length)] : enemyData[0];

            EnemyManager newEnemy = entry.SpawnObject(spawnTransform);
            spawnedEnemies.Add(newEnemy);
            newEnemy.Initialize(entry, this, creator);
        }
    }
}
