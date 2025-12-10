using UnityEngine;
using System.Collections;

namespace Onion_AI
{
    public class BossStats : CharacterStatistics
    {
        BossManager bossManager;

        //Status
        private bool showHealthBar;
        private WaitForSeconds waitForSeconds;
        public bool runAway {get; private set;}

        protected override void Awake()
        {
            base.Awake();
            bossManager = characterManager as BossManager;
        }

        protected override void Start()
        {
            base.Start();
            waitForSeconds = new WaitForSeconds(3.5f);
            bossManager.healthBarUI.gameObject.SetActive(false);
        }

        public override void CharacterStatistics_Update()
        {
            if(showHealthBar)
            {
                StartCoroutine(DisplayHealthBarCoroutine());
            }
            base.CharacterStatistics_Update();
        }

        //Functionalities 

        public override void ResetHealth()
        {
            base.ResetHealth();
        }

        public override void HandleDeath()
        {
            currentHealth = 0.0f;
            showHealthBar = false;
            characterManager.isDead = true;

            InstantiateCoin();
            GameManager gameManager = GameManager.Instance;
            gameManager.Audio.PlaySound(111);

            if (bossManager.appearanceCount >= 3)
            {
                LevelSpawners.RandomParticleEffect(transform.position, Quaternion.identity, gameManager.Level.explosionFXArray);
                characterManager.characterAnimationManager.PlayTargetAnimation(characterManager.characterAnimationManager.deathHash, true);
                Destroy(bossManager.gameObject, 5f);
                return;
            }
            runAway = true;
            characterManager.characterAnimationManager.PlayTargetAnimation(characterManager.characterAnimationManager.deathHash, true);
            Invoke(nameof(DeactivateObject), 3.5f);
        }

        private void DeactivateObject()
        {
            bossManager.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            runAway = false;
            
            Vector3 targetPosition = new Vector3(0, 6.55f, 0f);
            transform.SetLocalPositionAndRotation(targetPosition, Quaternion.identity);
            ResetHealth();
        }

        public override void TakeDamage(float damageValue)
        {
            if(currentHealth <= 0.0f)
            {
                HandleDeath();
                return;
            }
            showHealthBar = true;
            bossManager.healthBarUI.gameObject.SetActive(showHealthBar);
            base.TakeDamage(damageValue);
        }

        private void InstantiateCoin()
        {
            LevelSpawners levelSpawners = GameManager.Instance.Level;

            GoldCoin goldCoin = levelSpawners.goldObjectPool.Get();
            int coinCount = bossManager.appearanceCount * 100;

            goldCoin.CreateCoin(coinCount, GameManager.Instance.playerManager);
            goldCoin.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            goldCoin.levelSpawner = levelSpawners;
        }

        private IEnumerator DisplayHealthBarCoroutine()
        {
            while(showHealthBar == true)
            {
                yield return waitForSeconds;
                showHealthBar = false;
                bossManager.healthBarUI.gameObject.SetActive(showHealthBar);
            }
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
