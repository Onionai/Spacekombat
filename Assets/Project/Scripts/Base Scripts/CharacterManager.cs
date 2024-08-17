using UnityEngine;

namespace Onion_AI
{
    public class CharacterManager : MonoBehaviour
    {
        //In-Built Components
        public Rigidbody2D rigidBody {get; private set;}

        //OnionAI Components
        public UIBar healthBar {get; private set;}
        public CharacterCombat characterCombat {get; private set;}
        public CharacterMovement characterMovement {get; private set;}
        public CharacterStatistics characterStatistics {get; private set;}

        [Header("Status")]
        public bool isDead;
    
        protected virtual void Awake()
        {
            healthBar = GetComponent<UIBar>();
            rigidBody = GetComponent<Rigidbody2D>();

            characterCombat = GetComponent<CharacterCombat>();
            characterMovement = GetComponent<CharacterMovement>();
            characterStatistics = GetComponent<CharacterStatistics>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void FixedUpdate()
        {
            if(isDead)
            {
                return;
            }
            float delta = Time.deltaTime;
            characterMovement.CharacterMovement_FixedUpdate(delta);
        }

        protected virtual void Update()
        {
            if(isDead)
            {
                return;
            }
            characterCombat.CharacterCombat_Update();
            characterStatistics.CharacterStatistics_Update();
        }
    }
}
