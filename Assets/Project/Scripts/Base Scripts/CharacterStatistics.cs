using UnityEngine;

namespace Onion_AI
{
    public class CharacterStatistics : MonoBehaviour
    {
        protected CharacterManager characterManager;

        //Parameters
        private float healthLevel;
        public float currentHealth {get; private set;}

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            
        }

        public virtual void CharacterStatistics_Update()
        {
            
        }

        //Functionalities 

        public virtual void ResetHealth()
        {
            characterManager.isDead = false;
            currentHealth = healthLevel * 10.0f;

            characterManager.healthBar.SetMaxValue(currentHealth);
            characterManager.healthBar.SetCurrentValue(currentHealth);
        }

        public virtual void TakeDamage(float damageValue)
        {
            if(currentHealth <= 0.0f)
            {
                currentHealth = 0.0f;
                characterManager.isDead = true;
                return;
            }
            currentHealth -= damageValue;
            characterManager.healthBar.SetCurrentValue(currentHealth);
        }

        protected virtual void RegenerateStatisticProcedurally()
        {

        }

        public virtual void ReduceStatisticProcedurally(float damageValue)
        {

        }
    }
}
