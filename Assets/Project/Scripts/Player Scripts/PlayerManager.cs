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
        public int coinCount;

        protected override void Awake()
        {
            base.Awake();

            canShoot = false;
            playerInput = GetComponent<PlayerInput>();
            gameManager = FindObjectOfType<GameManager>();

            playerCombat = characterCombat as PlayerCombat;
            playerMovement = characterMovement as PlayerMovement;
            playerStatistic = characterStatistics as PlayerStatistic;
        }

        protected override void Start()
        {
            base.Start();

            healthBarUI = gameManager.uIManager.PlayerHealthBar;
            characterAnimationManager.PlayTargetAnimation(characterAnimationManager.spawnHash, true);
        }

        public void PlayReloadAnimation()
        {
            characterAnimationManager.PlayTargetAnimation(characterAnimationManager.reloadHash, true);
        }

        protected override void Update()
        {
            if(GameManager.gameState != GameState.Active)
            {
                return;
            }
            
            playerMovement.ClampPlayerPosition();

            if(isDead)
            {
                return;
            }

            playerInput.PlayerInput_Update();
            performingAction = animator.GetBool(characterAnimationManager.performActionHash);

            base.Update();
            gameManager.uIManager.DisplayCoinCount(coinCount);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if(isDead)
            {
                return;
            }

            if(other.CompareTag("Bullet"))
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
