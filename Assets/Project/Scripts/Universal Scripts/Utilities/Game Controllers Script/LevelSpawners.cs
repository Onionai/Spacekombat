using UnityEngine;
using UnityEngine.Pool;

namespace Onion_AI
{
    public class LevelSpawners : MonoBehaviour
    {
        [Header("Objects To Pool")]
        [SerializeField] private WeaponManager bulletObject;

        //Object Pools
        public ObjectPool<WeaponManager> bulletPool {get; private set;}

        public void Initialize()
        {
            bulletPool = ObjectSpawner.PoolWeapon(bulletObject);
        }
    }
}
