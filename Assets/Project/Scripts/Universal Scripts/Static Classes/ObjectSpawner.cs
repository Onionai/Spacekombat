using UnityEngine;
using UnityEngine.Pool;

namespace Onion_AI
{
    public static class ObjectSpawner
    {
        private static void SpawnersGetObject(Transform transform)
        {
            transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
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
                spawnObject => {SpawnersGetObject(spawnObject.transform);},
                spawnObject => {spawnObject.gameObject.SetActive(false);},
                spawnObject => {GameObject.Destroy(spawnObject.gameObject);},
                false, 150, 200
            );
            return objectPool;
        }

        public static ObjectPool<EnemyManagersController> PoolEnemyControllers(GameManager GM, EnemyManagersController objectToPool)
        {
            ObjectPool<EnemyManagersController> objectPool = new ObjectPool<EnemyManagersController>
            (
                () => {return GameObject.Instantiate(objectToPool);},
                spawnObject => {GetEnemyController(GM, spawnObject);},
                spawnObject => {spawnObject.gameObject.SetActive(false);},
                spawnObject => {GameObject.Destroy(spawnObject.gameObject);},
                false, 10, 20
            );
            return objectPool;
        }

        private static void GetEnemyController(GameManager GM, EnemyManagersController spawnObject)
        {
            spawnObject.InitializeController(GM);
            spawnObject.gameObject.SetActive(true);
        }
    }
}
