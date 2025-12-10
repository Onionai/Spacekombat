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
        [field: SerializeField] public EnemyController Controller {get; private set;}

        [field: Header("Status")]
        public bool attemptSuicide;
        public bool hasReachedTarget;
        public PathCreator PathCreatorClass { get; private set; }
        [field: SerializeField] public EnemyType enemyType {get; private set;}

        public void Initialize(Enemy_Data ED, EnemyController EMC, PathCreator PC)
        {
            enemyData = ED;
            Controller = EMC;
            PathCreatorClass = PC;
            characterStatistics.ResetHealth();
            enemyType = Controller.TypeOfEnemy;
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
            if(GameManager.Instance.CompareGameStatus(GamePlayState.Active) != true)
            {
                return;
            }
            base.Update();
        }

        public void ReleaseFromPool()
        {
            enemyMovement.ResetDistanceRemaining();
            if(Controller.spawnedEnemies.Contains(this))
            {
                Controller.spawnedEnemies.Remove(this);
            }
            enemyData.EnemyPool.Release(this);
        }
    }
}
