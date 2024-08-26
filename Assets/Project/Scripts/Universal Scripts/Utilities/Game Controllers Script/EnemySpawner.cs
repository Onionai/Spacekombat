using UnityEngine;

namespace Onion_AI
{
    public class EnemySpawner : MonoBehaviour
    {
        private float spawnRate;
        private float nextSpawnTime;

        [Header("Parameters")]
        public GameManager gameManager;
        public int spawnQuantity {get; private set;}
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float minimumWidth, maximumWidth;
        [SerializeField] private Spawnable_Items[] spawnable_Items;
        [field: SerializeField] public Vector2 spawnRateRange {get; private set;}
        [field: SerializeField] public Vector2 spawnQuantityRange {get; private set;}

        private void Awake()
        {
            spawnQuantity = (int)Random.Range(spawnQuantityRange.x, spawnQuantityRange.y);

            InitializeData();
        }

        public void EnemySpawn_Updater()
        {
            SpawnObject();
        }

        private void InitializeData()
        {
            for(int index = 0; index < spawnable_Items.Length; index++)
            {
                spawnable_Items[index] = Instantiate(spawnable_Items[index]);

                Spawnable_Items spawnable_Item = spawnable_Items[index];
                
                spawnable_Item.enemySpawner = this;
                spawnable_Item.Initialize(spawnQuantity, (int)spawnQuantityRange.y, spawnPoint);
            }
        }

        private void SpawnObject()
        {
            if(Time.time > nextSpawnTime)
            {
                spawnRate = Random.Range(spawnRateRange.x, spawnRateRange.y);

                nextSpawnTime = Time.time + spawnRate;
                Spawn();
            }
        }

        private void Spawn()
        {
            int randomIndex = Random.Range(0, spawnable_Items.Length);

            Spawnable_Items spawnItem = spawnable_Items[randomIndex];
            spawnItem.objectSpawner.IObjectSpawner_SpawnObject(minimumWidth, maximumWidth);
        }
    }
}
