using UnityEngine;
using UnityEngine.Pool;

namespace Onion_AI
{
    public static class ObjectSpawner
    {
        public static ObjectPool<GameObject> PoolObject(GameObject objectToPool)
        {
            ObjectPool<GameObject> objectPool = new ObjectPool<GameObject>
            (
                () => {return GameObject.Instantiate(objectToPool);},
                spawnObject => {spawnObject.gameObject.SetActive(true);},
                spawnObject => {spawnObject.gameObject.SetActive(false);},
                spawnObject => {GameObject.Destroy(spawnObject.gameObject);},
                false, 300, 500
            );
            return objectPool;
        }

        public static ObjectPool<WeaponManager> PoolWeapon(WeaponManager objectToPool)
        {
            ObjectPool<WeaponManager> objectPool = new ObjectPool<WeaponManager>
            (
                () => {return GameObject.Instantiate(objectToPool);},
                spawnObject => {spawnObject.gameObject.SetActive(true);},
                spawnObject => {spawnObject.gameObject.SetActive(false);},
                spawnObject => {GameObject.Destroy(spawnObject.gameObject);},
                false, 300, 500
            );
            return objectPool;
        }
    }
}
