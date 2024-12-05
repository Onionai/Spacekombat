using PathCreation;
using UnityEngine;

namespace Onion_AI
{
    public class EnemyManager : CharacterManager, IReleaseFromPool
    {
        //Onion_AI Components
        public EnemyCombat enemyCombat {get; private set;}
        public EnemyMovement enemyMovement {get; private set;}
        public EnemyStatistic enemyStatistic {get; private set;}

        [field: Header("Parameters")]
        public Enemy_Data enemyData {get; private set;}
        public SpawnPoint spawnPoint {get; private set;}
        public PathCreator pathCreator {get; private set;}
        [field: SerializeField] public EnemyManagersController enemyManagersController {get; private set;}

        [field: Header("Status")]
        public bool attemptSuicide;
        public bool hasReachedFormation;
        [field: SerializeField] public EnemyType enemyType {get; private set;}

        public void Initialize(Enemy_Data ED, EnemyManagersController EMC, SpawnPoint SP)
        {
            enemyData = ED;
            spawnPoint = SP;
            enemyManagersController = EMC;

            spawnPoint.spawnedEnemies.Add(this);
            enemyManagersController.spawnedEnemies.Add(this);
            enemyType = enemyManagersController.enemyType;

            gameManager = enemyManagersController.gameManager;
            if(enemyType == EnemyType.FreeRoam) {pathCreator = enemyManagersController.pathController.RandomPathCreator_FreeRoam();}

            enemyMovement.Initialize();
            if(isDead) {characterStatistics.ResetHealth();}
        }

        protected override void Awake()
        {
            base.Awake();
            
            enemyCombat = characterCombat as EnemyCombat;
            enemyMovement = characterMovement as EnemyMovement;
            enemyStatistic = characterStatistics as EnemyStatistic;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            if(GameManager.gameState != GameState.Active)
            {
                return;
            }
            base.Update();
        }

        public void ReleaseFromPool()
        {
            enemyMovement.ResetDistanceRemaining();
            enemyData.enemyPool.Release(this);
        }
    }
}
