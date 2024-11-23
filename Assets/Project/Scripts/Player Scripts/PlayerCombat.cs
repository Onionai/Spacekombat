using UnityEngine;

namespace Onion_AI
{
    public class PlayerCombat : CharacterCombat
    {
        //Manager
        [Header("Audio Sounds")]
        public AudioClip fireAudioClip;
        public AudioSource combatAudioSource;
        protected PlayerManager playerManager;
        
        [Header("Status")]
        [SerializeField] private WeaponData playerBulletData;

        // Update is called once per frame
        public override void Awake()
        {
            base.Awake();
            playerManager = characterManager as PlayerManager;
        }

        private void Start()
        {
            playerBulletData = Instantiate(playerBulletData);
            playerBulletData.InitializeWeaponData();
        }

        public override void CharacterCombat_Update(float delta)
        {
            base.CharacterCombat_Update(delta);
        }

        protected override void Shoot(float delta)
        {
            if(playerManager.canShoot != true)
            {
                return;
            }
            
            deltaTime += Time.deltaTime;
            float delayShotTime = 1 / fireRate;

            if(deltaTime < delayShotTime)
            {
                return;
            }

            base.Shoot(delta);
            StaticAudioManager.PlaySFX(combatAudioSource, fireAudioClip);

            deltaTime = 0.0f;
        }

        protected override void Fire(Transform firePoint)
        {
            WeaponManager fireObject = playerBulletData.bulletPool.Get();

            fireObject.SetWeaponData(playerBulletData);
            fireObject.levelSpawner = characterManager.gameManager.levelSpawners;

            fireObject.Initialize(firePoint, this);
        }
    }
}
