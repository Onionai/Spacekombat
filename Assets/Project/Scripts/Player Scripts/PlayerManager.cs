using UnityEngine;

namespace Onion_AI
{
    public class PlayerManager : CharacterManager
    {
        public bool HasShield { get; private set; }
        public bool HasMagnet { get; private set; }

        //Onion_AI Components
        public PlayerInput Input {get; private set;}
        public PlayerCombat Combat {get; private set;}
        public PlayerMovement Movement {get; private set;}
        public PlayerStatistic Statistics {get; private set;}

        [Header("Status")]
        public int coinCount;
        public int killCount;

        [Header("Power Up Parameters")]
        public float expirationTime;
        public PowerUpClass currentPowerUp;
        [SerializeField] private GameObject shieldObject;

        protected override void Awake()
        {
            base.Awake();

            canShoot = false;
            Input = GetComponent<PlayerInput>();

            Combat = characterCombat as PlayerCombat;
            Movement = characterMovement as PlayerMovement;
            Statistics = characterStatistics as PlayerStatistic;
        }

        protected override void Start()
        {
            base.Start();

            shieldObject.SetActive(HasShield);
            healthBarUI = GameManager.Instance.uiManager.PlayerHealthBar;
            characterAnimationManager.PlayTargetAnimation(characterAnimationManager.spawnHash, true);
        }

        public void PlayReloadAnimation()
        {
            characterAnimationManager.PlayTargetAnimation(characterAnimationManager.reloadHash, true);
        }

        public void HandleCoinMagnetism(bool status)
        {
            HasMagnet = status;
        }

        public void HandleShieldVisibility(bool status)
        {
            HasShield = status;
            shieldObject.SetActive(status);
        }

        private void HandlePowerUpCounter(float delta)
        {
            if(currentPowerUp == null || currentPowerUp.PowerUpType == PowerUpType.Health)
            {
                return;
            }

            if(expirationTime <= 0.0f)
            {
                currentPowerUp.EndPowerUp(this);
                return;
            }
            expirationTime -= delta;
        }

        protected override void Update()
        {
            if (GameManager.Instance.CompareGameStatus(GamePlayState.Active) != true)
            {
                return;
            }

            Movement.ClampPlayerPosition();

            if(isDead)
            {
                return;
            }

            Input.PlayerInput_Update();
            performingAction = animator.GetBool(characterAnimationManager.performActionHash);

            base.Update();
            HandlePowerUpCounter(Time.deltaTime);
            GameManager.Instance.uiManager.DisplayCoinCount(coinCount);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(isDead)
            {
                return;
            }

            if (other.CompareTag("PowerUp"))
            {
                PowerUpClass powerUpClass = other.GetComponent<PowerUpClass>();
                powerUpClass.ApplyPowerUp(this);
            }

            if (other.CompareTag("Bullet"))
            {
                return;
            }

            EnemyManager characterCausingDamage = other.GetComponentInParent<EnemyManager>();
            if(characterCausingDamage != null)
            {
                if (HasShield != true)
                {
                    characterStatistics.HandleDeath();
                }
                characterCausingDamage.characterStatistics.HandleDeath();
            }
        }
    }
}
