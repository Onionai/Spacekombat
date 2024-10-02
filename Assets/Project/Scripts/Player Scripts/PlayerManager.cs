using UnityEngine;

namespace Onion_AI
{
    public class PlayerManager : CharacterManager
    {
        //Onion_AI Components
        public PlayerInput playerInput {get; private set;}
        public PlayerCombat playerCombat {get; private set;}
        public PlayerMovement playerMovement {get; private set;}
        public PlayerStatistic playerStatistic {get; private set;}

        //status
        bool hasReloaded;
        public int coinCount;

        protected override void Awake()
        {
            base.Awake();
            playerInput = GetComponent<PlayerInput>();
            gameManager = FindObjectOfType<GameManager>();

            playerCombat = characterCombat as PlayerCombat;
            playerMovement = characterMovement as PlayerMovement;
            playerStatistic = characterStatistics as PlayerStatistic;
        }

        protected override void Start()
        {
            base.Start();
            characterAnimationManager.PlayTargetAnimation(characterAnimationManager.spawnHash, true);
        }

        public void PlayReloadAnimation()
        {
            if(hasReloaded != true)
            {
                characterAnimationManager.PlayTargetAnimation(characterAnimationManager.reloadHash, true);
                hasReloaded = true;
            }
        }

        protected override void Update()
        {
            playerMovement.ClampPlayerPosition();

            if(isDead)
            {
                return;
            }
            
            playerInput.PlayerInput_Update();
            base.Update();
            gameManager.uIManager.DisplayCoinCount(coinCount);
            performingAction = animator.GetBool(characterAnimationManager.performActionHash);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if(isDead)
            {
                return;
            }

            EnemyManager characterCausingDamage = other.GetComponentInParent<EnemyManager>();
            if(characterCausingDamage != null)
            {
                characterStatistics.HandleDeath();
            }
        }
    }
}
