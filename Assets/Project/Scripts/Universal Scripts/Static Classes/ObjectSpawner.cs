using UnityEngine;
using UnityEngine.Pool;

namespace Onion_AI
{
    public static class ObjectSpawner
    {
        private static void SpawnersGetObject(Transform transform)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        
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

        public static ObjectPool<GoldCoin> PoolGoldCoin(GoldCoin objectToPool)
        {
            ObjectPool<GoldCoin> objectPool = new ObjectPool<GoldCoin>
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

        public static ObjectPool<EnemyManager> PoolEnemyManager(int quantity, int maxQuantity, EnemyManager objectToPool)
        {
            ObjectPool<EnemyManager> objectPool = new ObjectPool<EnemyManager>
            (
                () => {return GameObject.Instantiate(objectToPool);},
                spawnObject => {SpawnersGetObject(spawnObject.transform);},
                spawnObject => {spawnObject.gameObject.SetActive(false);},
                spawnObject => {GameObject.Destroy(spawnObject.gameObject);},
                false, quantity, maxQuantity
            );
            return objectPool;
        }

        public static ObjectPool<EnemyManagersController> PoolEnemyControllers(EnemySpawner ES, GameManager GM, EnemyManagersController objectToPool)
        {
            ObjectPool<EnemyManagersController> objectPool = new ObjectPool<EnemyManagersController>
            (
                () => {return GameObject.Instantiate(objectToPool);},
                spawnObject => {GetEnemyController(ES, GM, spawnObject);},
                spawnObject => {spawnObject.gameObject.SetActive(false);},
                spawnObject => {GameObject.Destroy(spawnObject.gameObject);},
                false, 10, 20
            );
            return objectPool;
        }

        private static void GetEnemyController(EnemySpawner ES, GameManager GM, EnemyManagersController spawnObject)
        {
            spawnObject.InitializeController(ES, GM);
            spawnObject.gameObject.SetActive(true);
        }
    }
}
