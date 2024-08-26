using UnityEngine;

namespace Onion_AI
{
    public class PlayerCombat : CharacterCombat
    {
        //Manager
        protected PlayerManager playerManager;

        // Update is called once per frame
        public override void Awake()
        {
            base.Awake();
            playerManager = characterManager as PlayerManager;
        }

        public override void CharacterCombat_Update(float delta)
        {
            deltaTime += delta;
            float delayShotTime = 1 / fireRate;

            if(deltaTime < delayShotTime)
            {
                return;
            }
            base.CharacterCombat_Update(delta);
        }

        protected override void Shoot(float delta)
        {
            base.Shoot(delta);
        }

        protected override void Fire(Transform firePoint)
        {
            if(playerManager.isMoving != true)
            {
                return;
            }
            
            base.Fire(firePoint);
        }
    }
}
