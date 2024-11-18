using UnityEngine;
using System.Collections;

namespace Onion_AI
{
    public class PlayerStatistic : CharacterStatistics
    {
        PlayerManager playerManager;
        [SerializeField] private float delaySeconds;
        private WaitForSeconds delayBeforeDisplayingExitPanel;

        protected override void Awake()
        {
            base.Awake();
            playerManager = characterManager as PlayerManager;
        }

        protected override void Start()
        {
            base.Start();
            delayBeforeDisplayingExitPanel = new WaitForSeconds(delaySeconds);
        }

        public override void CharacterStatistics_Update()
        {
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
                HandleDeath();
                return;
            }
            base.TakeDamage(damageValue);
        }

        public override void HandleDeath()
        {
            base.HandleDeath();
            currentHealth = 0.0f;
            characterManager.isDead = true;
            
            HealthCounterManager.Instance.ReduceHealthCounter();
            characterManager.characterAnimationManager.PlayTargetAnimation(characterManager.characterAnimationManager.deathHash, true);

            StartCoroutine(DisplayExitMenu());
        }

        private IEnumerator DisplayExitMenu()
        {
            yield return delayBeforeDisplayingExitPanel;
            characterManager.gameManager.uIManager.DisplayExitMenu(true);
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
