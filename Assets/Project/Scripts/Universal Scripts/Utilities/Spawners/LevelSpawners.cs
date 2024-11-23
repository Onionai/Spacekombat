using UnityEngine;
using UnityEngine.Pool;

namespace Onion_AI
{
    public class LevelSpawners : MonoBehaviour
    {
        [Header("Objects To Pool")]
        [SerializeField] private GoldCoin goldCoinObject;
        [SerializeField] private WeaponData[] weaponDataArray;

        [Header("Particles Array")]
        public ParticleSystem[] impactFXArray;
        public ParticleSystem[] explosionFXArray;

        //Object Pools
        public ObjectPool<GoldCoin> goldObjectPool {get; private set;}

        public void Initialize()
        {
            for(int i = 0; i < weaponDataArray.Length; i++)
            {
                WeaponData weaponData = Instantiate(weaponDataArray[i]);

                weaponDataArray[i] = weaponData;
                weaponData.InitializeWeaponData();
            }
            goldObjectPool = ObjectSpawner.PoolGoldCoin(goldCoinObject);
        }

        public WeaponManager RandomBulletShooter()
        {
            int random = Random.Range(0, weaponDataArray.Length);

            WeaponManager bullet = weaponDataArray[random].bulletPool.Get();
            bullet.SetWeaponData(weaponDataArray[random]);
            return bullet;
        }

        public static void RandomParticleEffect(Vector3 position, Quaternion rotation, ParticleSystem[] particleSystems)
        {
            int random = Random.Range(0, particleSystems.Length);

            ParticleSystem particleSystem = Instantiate(particleSystems[random]);
            particleSystem.transform.SetPositionAndRotation(position, rotation);
        }
    }
}
