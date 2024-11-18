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
            base.CharacterCombat_Update(delta);
        }

        protected override void Shoot(float delta)
        {
            deltaTime += Time.deltaTime;
            float delayShotTime = 1 / fireRate;

            if (deltaTime < delayShotTime)
            {
                return;
            }
            base.Shoot(delta);
            deltaTime = 0.0f;
        }

        protected override void Fire(Transform firePoint)
        {
            base.Fire(firePoint);
        }
    }
}
