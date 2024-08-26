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

        public override void TakeDamage(float damageValue)
        {
            if(currentHealth <= 0.0f)
            {
                currentHealth = 0.0f;
                showHealthBar = false;
                characterManager.isDead = true;
                enemyManager.gameManager.KilledTarget();
                enemyManager.enemyData.enemyPool.Release(enemyManager);
                return;
            }
            showHealthBar = true;
            enemyManager.healthBarUI.gameObject.SetActive(showHealthBar);
            base.TakeDamage(damageValue);
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
