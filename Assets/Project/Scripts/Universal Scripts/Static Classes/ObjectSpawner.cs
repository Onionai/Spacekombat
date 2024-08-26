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

        private static T GetComponent<T>(GameObject gameObject) where T : Component
        {
            // Attempt to get the component of type T from the GameObject
            T component = gameObject.GetComponent<T>();
            return component;
        }

        private static void SpawnersGetObject(Transform transform, Transform spawnPoint)
        {
            transform.SetParent(spawnPoint);
            transform.localScale = new Vector3(1,1,1);
        }

        public static ObjectPool<EnemyManager> PoolEnemyManager(int quantity, int maxQuantity, Transform spawnPoint, EnemyManager objectToPool)
        {
            ObjectPool<EnemyManager> objectPool = new ObjectPool<EnemyManager>
            (
                () => {return GameObject.Instantiate(objectToPool);},
                spawnObject => {SpawnersGetObject(spawnObject.transform, spawnPoint);},
                spawnObject => {spawnObject.gameObject.SetActive(false);},
                spawnObject => {GameObject.Destroy(spawnObject.gameObject);},
                false, quantity, maxQuantity
            );
            return objectPool;
        }

        public static ObjectPool<EnemyManager> PoolEnemyManager(EnemyManager objectToPool)
        {
            ObjectPool<EnemyManager> objectPool = new ObjectPool<EnemyManager>
            (
                () => {return GameObject.Instantiate(objectToPool);},
                spawnObject => {spawnObject.gameObject.SetActive(true);},
                spawnObject => {spawnObject.gameObject.SetActive(false);},
                spawnObject => {GameObject.Destroy(spawnObject.gameObject);},
                false, 40, 55
            );
            return objectPool;
        }
    }
}
