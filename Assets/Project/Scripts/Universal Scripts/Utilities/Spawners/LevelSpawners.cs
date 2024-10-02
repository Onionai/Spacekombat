using UnityEngine;
using UnityEngine.Pool;

namespace Onion_AI
{
    public class LevelSpawners : MonoBehaviour
    {
        [Header("Objects To Pool")]
        [SerializeField] private GoldCoin goldCoinObject;
        [SerializeField] private WeaponManager bulletObject;

        //Object Pools
        public ObjectPool<GoldCoin> goldObjectPool {get; private set;}
        public ObjectPool<WeaponManager> bulletPool {get; private set;}

        public void Initialize()
        {
            bulletPool = ObjectSpawner.PoolWeapon(bulletObject);
            goldObjectPool = ObjectSpawner.PoolGoldCoin(goldCoinObject);
        }
    }
}
