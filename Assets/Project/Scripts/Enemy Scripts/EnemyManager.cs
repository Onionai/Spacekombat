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
        [field: SerializeField] public EnemyManagersController enemyManagersController {get; private set;}

        [field: Header("Status")]
        public bool canShoot;
        [field: SerializeField] public bool hasSetPath {get; private set;}
        [field: SerializeField] public EnemyType enemyType {get; private set;}

        public void Initialize(Enemy_Data ED, EnemyManagersController EMC, SpawnPoint SP)
        {
            enemyData = ED;
            spawnPoint = SP;
            enemyManagersController = EMC;
            enemyType = enemyManagersController.enemyType;
            gameManager = enemyManagersController.gameManager;

            enemyMovement.Initialize();
            enemyType = enemyManagersController.enemyType;
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
            base.Update();
        }

        public void SetEnemyFirstPath(bool setPath)
        {
            hasSetPath = setPath;
        }

        public void ReleaseFromPool()
        {
            enemyData.enemyPool.Release(this);
        }
    }
}
