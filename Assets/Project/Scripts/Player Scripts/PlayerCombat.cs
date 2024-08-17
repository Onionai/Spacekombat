using UnityEngine;

namespace Onion_AI
{
    public class PlayerCombat : CharacterCombat
    {
        //Manager
        protected PlayerManager playerManager;
        public bool shoot;

        // Update is called once per frame
        public override void Awake()
        {
            base.Awake();
            playerManager = characterManager as PlayerManager;
        }

        public override void CharacterCombat_Update()
        {
            if(shoot)
            {
                Shoot();
            }
            
            base.CharacterCombat_Update();
        }

        protected override void Shoot()
        {
            foreach(Transform firePoint in firePoints)
            {
                Fire(firePoint);
            }
        }

        protected override void Fire(Transform firePoint)
        {
            WeaponManager fireObject = playerManager.gameManager.levelSpawners.bulletPool.Get();

            fireObject.Initialize(firePoint, this);
        }
    }
}
