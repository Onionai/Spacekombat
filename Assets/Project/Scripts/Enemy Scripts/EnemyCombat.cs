using UnityEngine;

namespace Onion_AI
{
    public class EnemyCombat : CharacterCombat
    {
        protected EnemyManager enemyManager;

        public override void Awake()
        {
            base.Awake();
            enemyManager = characterManager as EnemyManager;
        }

        public override void CharacterCombat_Update(float delta)
        {
            if(enemyManager.canShoot != true)
            {
                return;
            }
            
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
            base.Fire(firePoint);
        }
    }
}
