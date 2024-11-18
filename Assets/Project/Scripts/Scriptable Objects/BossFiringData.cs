using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Onion_AI
{
    [CreateAssetMenu(fileName = "Boss_Firing_Data", menuName = "OnionAI/BossFiringData")]
    public class BossFiringData : ScriptableObject
    {
        private float deltaTime;
        private BossCombat bossCombat;
        private List<Transform> firePoints;
        private LevelSpawners levelSpawners;

        [Header("Parameters")]
        public BossFiringType bossFiringType;
        [SerializeField] private WeaponData[] bossBulletData;

        public void Initialize(BossCombat BC)
        {
            bossCombat = BC;
            firePoints = BC.firePoints;
            levelSpawners = BC.GetComponentInParent<LevelSpawners>();

            for (int i = 0; i < bossBulletData.Length; i++)
            {
                WeaponData bossBullet = Instantiate(bossBulletData[i]);

                bossBulletData[i] = bossBullet;
                bossBullet.InitializeWeaponData();
            }
        }

        public void HandleFiring(float delta, float fireRate)
        {
            if (bossCombat.bossManager.canShoot != true)
            {
                return;
            }

            deltaTime += delta;
            float delayShotTime = 1 / fireRate;

            if (deltaTime < delayShotTime)
            {
                return;
            }

            if (bossFiringType == BossFiringType.Static)
            {
                HandleStatic_Shooting();
            }
            else if (bossFiringType == BossFiringType.Singular)
            {
                HandleSingle_Shooting();
            }
            else
            {
                HandleContinuos_Shooting();
            }
            deltaTime = 0.0f;
        }

        private WeaponManager FireBullet(WeaponData weaponData)
        {
            WeaponManager bulletManager = weaponData.bulletPool.Get();

            bulletManager.SetWeaponData(weaponData);
            bulletManager.levelSpawner = levelSpawners;

            return bulletManager;
        }

        private void HandleSingle_Shooting()
        {
            foreach (Transform firePoint in firePoints)
            {
                if (firePoints.IndexOf(firePoint) == 0)
                {
                    continue;
                }

                WeaponManager bullet = FireBullet(bossBulletData[0]);
                bullet.Initialize(firePoint, bossCombat);
            }
        }

        private void HandleStatic_Shooting()
        {
            WeaponManager bullet = FireBullet(bossBulletData[0]);

            bullet.Initialize(firePoints[0], bossCombat);
        }

        private void HandleContinuos_Shooting()
        {
            foreach (Transform firePoint in firePoints)
            {
                if (firePoints.IndexOf(firePoint) == 0)
                {
                    WeaponManager bullet = FireBullet(bossBulletData[0]);
                    bullet.Initialize(firePoint, bossCombat);
                }
                else
                {
                    WeaponManager bullet = FireBullet(bossBulletData[1]);
                    bullet.Initialize(firePoint, bossCombat);
                }
            }
        }
    }
}
