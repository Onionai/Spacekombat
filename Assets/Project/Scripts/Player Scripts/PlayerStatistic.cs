using UnityEngine;

namespace Onion_AI
{
    public class PlayerStatistic : CharacterStatistics
    {
        PlayerManager playerManager;

        protected override void Awake()
        {
            base.Awake();
            playerManager = characterManager as PlayerManager;
        }

        protected override void Start()
        {
            base.Start();
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
            characterManager.characterAnimationManager.PlayTargetAnimation(characterManager.characterAnimationManager.deathHash, true);
            //Show Exit Dialog
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
