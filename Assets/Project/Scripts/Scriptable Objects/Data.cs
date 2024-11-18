using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

namespace Onion_AI
{
    [CreateAssetMenu(fileName = "Weapon_Data", menuName = "OnionAI/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private WeaponManager bulletObject;
        public ObjectPool<WeaponManager> bulletPool {get; private set;}

        public void InitializeWeaponData()
        {
            bulletPool = ObjectSpawner.PoolWeapon(bulletObject);
        }

        public void ReleaseObject(WeaponManager objectToRelease)
        {
            bulletPool.Release(objectToRelease);
        }
    }
}
