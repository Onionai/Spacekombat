using UnityEngine;
using System.Linq;
using Tarodev_Formations;
using System.Collections.Generic;

namespace Onion_AI
{
    public class EnemyManagersController : MonoBehaviour
    {
        public int numberOfSpawns;
        protected float nextSpawnTime;
        [SerializeField] protected int spawnQuantity;

        //Formation Setter
        private List<Vector3> formationPoints = new();
        private FormationController formationController;

        [Header("Components")]
        public GameManager gameManager;
        public EnemySpawner enemySpawner;

        [field: Header("Parameters")]
        public float spawnRate;
        protected int killedEnemies;
        public CharacterManager target;
        protected List<EnemyManager> spawnedEnemies = new();
        [SerializeField] protected Transform spawnPointHolder;
        [SerializeField] protected List<SpawnPoint> spawnPoints = new();
        
        [field: Header("Status")]
        public bool hasBeenSet;
        public EnemyType enemyType = EnemyType.Static;
        public MissionStatus missionStatus = MissionStatus.Active;

        protected virtual void Awake()
        {
            GetSpawnPoints();
            spawnQuantity = SpawnQuantity();
            formationController = GetComponent<FormationController>();

            formationController.RandomizeParameters();

            int random = (formationController.formationType == FormationType.Box) ? spawnQuantity : Random.Range(0,4);
            formationController.Initialize(random, spawnQuantity);
        }

        private int SpawnQuantity()
        {
            int random = Random.Range(0,7);

            if(random >= 0 && random < 2)
            {
                return 6;
            }
            else if(random >= 2 && random < 4)
            {
                return 12;
            }
            return 18;
        }

        private void Start()
        {
            for(int i = 0; i < spawnPoints.Count; i++)
            {
                SpawnPoint spawnPoint = spawnPoints[i];
                spawnPoint.Initialize(spawnQuantity);
            }
        }

        public virtual void InitializeMovement(EnemyManager enemyManager)
        {

        }

        protected void GetSpawnPoints()
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

        public virtual void EnemyManagerController_Updater()
        {
            if(missionStatus != MissionStatus.Active)
            {
                enemySpawner.enemyManagerControllerPool.Release(this);
                return;
            }

            SpawnEnemies();
            ChangeStatus();

            HandleFormation(Time.deltaTime);
            WaitTillEnemiesSet();
        }

        private void WaitTillEnemiesSet()
        {
            if(enemyType == EnemyType.Static || enemyType == EnemyType.FreeRoam)
            {
                return;
            }

            if(numberOfSpawns >= spawnQuantity)
            {
                return;
            }

            foreach(SpawnPoint spawnPoint in spawnPoints)
            {
                if(hasBeenSet)
                {
                    spawnPoint.RandomShooting();
                }
            }
        }

        protected virtual void ChangeStatus()
        {
            if(target.isDead)
            {
                missionStatus = MissionStatus.Completed;
            }
            else if(killedEnemies >= spawnQuantity)
            {
                missionStatus = MissionStatus.Failed;
                enemySpawner.currentEnemyManagersController = null;
            }
        }        

        protected virtual void SpawnEnemies()
        {
            if(numberOfSpawns >= spawnQuantity)
            {
                FillSpawnedEnemies();
                return;
            }

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

        //Functionalities
        #region Public Functions

        public void KilledTarget()
        {
            killedEnemies += 1;
        }

        public virtual void InitializeController(EnemySpawner ES, GameManager GM)
        {
            gameManager = GM;
            enemySpawner = ES;
            killedEnemies = 0;
            numberOfSpawns = 0;
            hasBeenSet = false;
            target = gameManager.playerManager;

            for(int i = 0; i < spawnPoints.Count; i++)
            {
                SpawnPoint spawnPoint = spawnPoints[i];
                spawnPoint.GetPathWayList();
            }
            missionStatus = MissionStatus.Active;
        }

        #endregion

        #region Movement and Formations

        private void FillSpawnedEnemies()
        {
            if(enemyType == EnemyType.Static || hasBeenSet)
            {
                return;
            }

            spawnedEnemies.Clear();
            foreach(SpawnPoint spawnPoint in spawnPoints)
            {
                spawnedEnemies.AddRange(spawnPoint.spawnedEnemies);
            }

            hasBeenSet = true;
        }

        private void HandleFormation(float delta)
        {
            formationPoints = formationController.EvaluatePoints().ToList();

            for(int i = 0; i < spawnedEnemies.Count; i++)
            {
                Transform spawnedTransform = spawnedEnemies[i].transform;
                EnemyMovement spawnedEnemy = spawnedEnemies[i].enemyMovement;

                float speed = spawnedEnemy.acceleration * spawnedEnemy.movementSpeed * delta;
                spawnedTransform.position = Vector3.MoveTowards(spawnedTransform.position, transform.position + formationPoints[i], speed);
            }
        }

        #endregion
    }
}
