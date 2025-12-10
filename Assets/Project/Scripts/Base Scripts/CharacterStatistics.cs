using UnityEngine;

namespace Onion_AI
{
    public class CharacterStatistics : MonoBehaviour
    {
        protected CharacterManager characterManager;

        //Parameters
        [SerializeField] protected float healthLevel;
        public float currentHealth {get; protected set;}

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
            
            UIBar healthBarUI = characterManager.healthBarUI;
            if (healthBarUI != null)
            {
                healthBarUI.SetMaxValue(currentHealth);
                healthBarUI.SetCurrentValue(currentHealth);
            }
        }

        public virtual void HandleDeath()
        {
            currentHealth = 0.0f;
            characterManager.isDead = true;
        }

        public virtual void TakeDamage(float damageValue)
        {
            currentHealth -= damageValue;
            characterManager.healthBarUI.SetCurrentValue(currentHealth);
        }

        protected virtual void RegenerateStatisticProcedurally()
        {

        }

        public virtual void ReduceStatisticProcedurally(float damageValue)
        {

        }
    }
}
