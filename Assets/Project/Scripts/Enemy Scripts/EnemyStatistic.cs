using UnityEngine;
using System.Collections;

namespace Onion_AI
{
    public class EnemyStatistic : CharacterStatistics
    {
        EnemyManager enemyManager;

        //Status
        private bool showHealthBar;
        private WaitForSeconds waitForSeconds;

        protected override void Awake()
        {
            base.Awake();
            enemyManager = characterManager as EnemyManager;
        }

        protected override void Start()
        {
            base.Start();
            waitForSeconds = new WaitForSeconds(3.5f);
            enemyManager.healthBarUI.gameObject.SetActive(false);
        }

        public override void CharacterStatistics_Update()
        {
            if(showHealthBar)
            {
                StartCoroutine(DisplayHealthBarCoroutine());
            }
            base.CharacterStatistics_Update();
        }

        //Functionalities 

        public override void ResetHealth()
        {
            base.ResetHealth();
        }

        public override void HandleDeath()
        {
            base.HandleDeath();
            showHealthBar = false;
            GameManager gameManager = GameManager.Instance;

            InstantiateCoin();
            //gameManager.totalScore += 50;
            gameManager.Audio.PlaySound(111);

            Vector3 position = transform.position;
            enemyManager.Controller.KilledEnemy(position);
            LevelSpawners.RandomParticleEffect(position, Quaternion.identity, gameManager.Level.explosionFXArray);

            enemyManager.ReleaseFromPool();
        }

        public override void TakeDamage(float damageValue)
        {
            if(currentHealth <= 0.0f)
            {
                HandleDeath();
                return;
            }
            showHealthBar = true;
            enemyManager.healthBarUI.gameObject.SetActive(showHealthBar);
            base.TakeDamage(damageValue);
        }

        private void InstantiateCoin()
        {
            LevelSpawners levelSpawners = GameManager.Instance.Level;
            GoldCoin goldCoin = levelSpawners.goldObjectPool.Get();

            goldCoin.CreateCoin(10, GameManager.Instance.playerManager);
            goldCoin.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            goldCoin.levelSpawner = levelSpawners;
        }

        private IEnumerator DisplayHealthBarCoroutine()
        {
            while(showHealthBar == true)
            {
                yield return waitForSeconds;
                showHealthBar = false;
                enemyManager.healthBarUI.gameObject.SetActive(showHealthBar);
            }
        }

        protected override void RegenerateStatisticProcedurally()
        {
            base.RegenerateStatisticProcedurally();
        }

        public override void ReduceStatisticProcedurally(float damageValue)
        {
            base.ReduceStatisticProcedurally(damageValue);
        }
    }
}
