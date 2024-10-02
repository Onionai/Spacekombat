using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

namespace Onion_AI
{
    public class EnemySpawner : MonoBehaviour
    {
        private int roundCount;
        private int numberOfSpawns;


        [Header("Components")]
        public GameManager gameManager;
        
        [field: Header("Parameters")]
        public Transform spawnPoint;
        public bool missionAccomplished {get; protected set;}
        [field: SerializeField] public int spawnQuantity {get; protected set;}

        [Header("Spawn Controllers")]
        public EnemyManagersController currentEnemyManagersController;
        public ObjectPool<EnemyManagersController> enemyManagerControllerPool;
        [SerializeField] private EnemyManagersController[] enemyManagersControllers;
        

        protected virtual void Awake()
        {
            roundCount = 1;
            spawnQuantity = Random.Range(7,11);
        }

        private void Start()
        {
            EnemyManagersController randomObject = RandomObject();
            enemyManagerControllerPool = ObjectSpawner.PoolEnemyControllers(this, gameManager, randomObject);
        }

        public virtual void EnemySpawn_Updater()
        {
            SpawnObject();
            if(currentEnemyManagersController != null)
            {
                currentEnemyManagersController.EnemyManagerController_Updater();
            }
        }

        private EnemyManagersController RandomObject()
        {
            int random = Random.Range(0, enemyManagersControllers.Length);
            return enemyManagersControllers[random];
        }

        protected void SpawnObject()
        {
            if(gameManager.playerManager.isDead)
            {
                return;
            }
            
            if(numberOfSpawns >= spawnQuantity)
            {
                missionAccomplished = true;
                return;
            }

            if(currentEnemyManagersController != null)
            {
                return;
            }

            gameManager.uIManager.SetRoundCount(roundCount);
            gameManager.playerManager.PlayReloadAnimation();
            Spawn();
        }

        protected void Spawn()
        {
            //yield return waitForSeconds;

            if(gameManager.canProceed == true)
            {
                roundCount++;
                gameManager.canProceed = false;

                currentEnemyManagersController = enemyManagerControllerPool.Get();
                currentEnemyManagersController.transform.parent = spawnPoint;
                currentEnemyManagersController.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            }
        }
    }
}
