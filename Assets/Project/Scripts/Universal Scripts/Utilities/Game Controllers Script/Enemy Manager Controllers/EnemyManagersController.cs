using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    public class EnemyManagersController : MonoBehaviour
    {
        protected float nextSpawnTime;

        //Components
        [HideInInspector] public CharacterManager target;
        public GameManager gameManager {get; private set;}
        public List<EnemyManager> spawnedEnemies {get; protected set;} = new();

        [HideInInspector]
        public EnemyManagerController_Data enemyManagerController_Data;

        [field: Header("Components")]
        [SerializeField] protected List<SpawnPoint> spawnPoints = new();
        [field: SerializeField] public PathController pathController {get; private set;}
        
    
        [Header("Spawn Parameters")]
        public float spawnRate;
        public int numberOfSpawns;
        public bool hasSetSpawnQuantity;
        [SerializeField] protected int spawnQuantity;
        [SerializeField] protected int spawnRoundCount;

        [field: Header("Parameters")]
        protected int killedEnemies;
        [SerializeField] protected float defaultDelayTimer;
        [SerializeField] protected Transform spawnPointHolder;
        
        [field: Header("Status")]
        public EnemyType enemyType = EnemyType.Static;
        public MissionStatus missionStatus = MissionStatus.Active;

        protected virtual void Awake()
        {
            foreach(Transform child in spawnPointHolder)
            {
                SpawnPoint spawnPoint = child.GetComponent<SpawnPoint>();
                if(spawnPoint != null)
                {
                    spawnPoints.Add(spawnPoint);
                }
            }
        }

        protected virtual void Start()
        {
            if(enemyType != EnemyType.Linear)
            {
                spawnedEnemies.Clear();
                SetSpawnQuantityAndFormationParameters();
                PrepareSpawnPoints();
            }
        }


        public virtual void InitializeController(GameManager GM)
        {
            gameManager = GM;
            target = gameManager.playerManager;

            //Game Rules
            killedEnemies = 0;
            numberOfSpawns = 0;
            missionStatus = MissionStatus.Active;
            hasSetSpawnQuantity = false;
        }

        protected virtual int SpawnQuantity()
        {
            int random = Random.Range(0,8);

            if(random >= 0 && random < 3)
            {
                return 36;
            }
            if(random >= 3 && random < 6)
            {
                return 48;
            }
            return 60;
        }

        public virtual void EnemyManagerController_Updater()
        {

        }       

        protected virtual void SetSpawnQuantityAndFormationParameters()
        {
            hasSetSpawnQuantity = true;
            spawnQuantity = SpawnQuantity();
        }

        protected virtual void PrepareSpawnPoints()
        {
            
        }

        protected virtual void SpawnEnemies()
        {
            if(Time.time <= nextSpawnTime)
            {
                return;
            }

            nextSpawnTime = Time.time + spawnRate;
            Spawn();
        }

        protected virtual void Spawn()
        {
            
        }

        public virtual void ReleaseObject()
        {
            enemyManagerController_Data.enemyManagerControllerPool.Release(this);
        }

        //Functionalities
        #region Public Functions

        public void KilledTarget()
        {
            killedEnemies += 1;
        }

        #endregion
    }
}
