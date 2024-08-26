using UnityEngine;

namespace Onion_AI
{
    public class CharacterStatistics : MonoBehaviour
    {
        protected CharacterManager characterManager;

        //Parameters
        [SerializeField] private float healthLevel;
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

            characterManager.healthBarUI?.SetMaxValue(currentHealth);
            characterManager.healthBarUI?.SetCurrentValue(currentHealth);
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
