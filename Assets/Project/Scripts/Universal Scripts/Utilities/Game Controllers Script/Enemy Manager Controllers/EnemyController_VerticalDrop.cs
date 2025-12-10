using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Onion_AI
{
    public class EnemyController_VerticalDrop : EnemyController
    {
        [Tooltip("If >0, spawn points inside a session will be staggered by this many seconds. 0 = simultaneous.")]
        [SerializeField] private float perSpawnStagger = 0f;

        public override void Init(EnemySpawner enemySpawner)
        {
            base.Init(enemySpawner);
            MaxKillCount = SetKillCount(15.0f);
        }

        protected override IEnumerator SpawnRoutine()
        {
            float elapsedSeconds = 0f;
            int totalPoints = spawnPoints.Length;

            while (currentKillCount < MaxKillCount)
            {
                float x = (spawnDecayMode == SpawnDecayMode.ByTimeSeconds) ? elapsedSeconds : (float)currentKillCount;

                float interval;
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

                int maxSpawnThisSession = Mathf.Min(4, totalPoints);
                int spawnCount = Random.Range(2, maxSpawnThisSession + 1);
                if (maxConcurrentEnemies > 0 && maxConcurrentEnemies < 2)
                {
                    spawnCount = Mathf.Clamp(spawnCount, 1, maxConcurrentEnemies);
                }

                if (maxConcurrentEnemies > 0)
                {
                    while (spawnedEnemies.Count + spawnCount > maxConcurrentEnemies)
                    {
                        if (currentKillCount >= MaxKillCount)
                        {
                            EnemyMissionFailed();
                            break;
                        }
                        yield return null;
                    }
                }

                if (currentKillCount >= MaxKillCount)
                {
                    EnemyMissionFailed();
                    break;
                }
                var indices = PickUniqueIndices(totalPoints, spawnCount);
                foreach (int idx in indices)
                {
                    SpawnOne_Pooled(idx, null);
                    if (perSpawnStagger > 0f)
                    {
                        yield return new WaitForSecondsRealtime(perSpawnStagger);
                    }
                }
                elapsedSeconds += interval;
                yield return new WaitForSecondsRealtime(interval);
            }
            spawnRoutine = null;
            EnemyMissionFailed();
            yield break;
        }

        private List<int> PickUniqueIndices(int total, int count)
        {
            count = Mathf.Clamp(count, 0, total);

            List<int> pool = new(total);
            for (int i = 0; i < total; i++)
            {
                pool.Add(i);
            }

            List<int> chosen = new(count);
            for (int i = 0; i < count; i++)
            {
                int j = Random.Range(0, pool.Count);
                chosen.Add(pool[j]);
                pool.RemoveAt(j);
            }
            return chosen;
        }
    }
}
