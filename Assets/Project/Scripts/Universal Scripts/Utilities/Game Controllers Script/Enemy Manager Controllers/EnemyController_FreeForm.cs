using UnityEngine;
using PathCreation;
using System.Collections;

namespace Onion_AI
{
    public class EnemyController_FreeForm : EnemyController
    {
        public override void Init(EnemySpawner enemySpawner)
        {
            base.Init(enemySpawner);
            MaxKillCount = SetKillCount(10.0f);
        }

        protected override IEnumerator SpawnRoutine()
        {
            float elapsedSeconds = 0f;
            while (currentKillCount < MaxKillCount)
            {
                if (maxConcurrentEnemies > 0 && spawnedEnemies.Count >= maxConcurrentEnemies)
                {
                    yield return null;
                    continue;
                }

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
                SpawnOne_Pooled(0, Path.RandomPathCreator_FreeRoam());
                elapsedSeconds += interval;
                yield return new WaitForSecondsRealtime(interval);
            }
            spawnRoutine = null;
            EnemyMissionFailed();
            yield break;
        }
    }
}
