using UnityEngine;

namespace Onion_AI
{
    public class CharacterManager : MonoBehaviour
    {
        //In-Built Components
        public Animator animator {get; private set;}
        public Rigidbody2D rigidBody {get; private set;}

        //OnionAI Components
        public GameManager gameManager {get; protected set;}
        public CharacterCombat characterCombat {get; private set;}
        public CharacterMovement characterMovement {get; private set;}
        public CharacterStatistics characterStatistics {get; private set;}
        public CharacterAnimationManager characterAnimationManager {get; private set;}

        [field: Header("Components")]
        [field: SerializeField] public UIBar healthBarUI {get; protected set;}

        [Header("Status")]
        public bool isDead;
        public bool canShoot;
        public bool isMoving;
        public bool performingAction {get; protected set;}
        [field: SerializeField] public CharacterType characterType {get; private set;} = CharacterType.Enemy;
    
        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();

            characterMovement = GetComponent<CharacterMovement>();
            characterStatistics = GetComponent<CharacterStatistics>();
            characterCombat = GetComponentInChildren<CharacterCombat>();
            characterAnimationManager = GetComponent<CharacterAnimationManager>();
        }

        protected virtual void Start()
        {
            characterStatistics?.ResetHealth();
            characterAnimationManager?.SetHashParameters();
        }

        protected virtual void FixedUpdate()
        {
            if(isDead)
            {
                return;
            }

            if(GameManager.gameState != GameState.Active)
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

            if(GameManager.gameState != GameState.Active)
            {
                return;
            }
            float delta = Time.deltaTime;

            characterCombat.CharacterCombat_Update(delta);
            characterStatistics.CharacterStatistics_Update();
            characterMovement.CharacterMovement_Update(delta);
        }
    }
}
