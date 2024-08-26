using UnityEngine;
using UnityEngine.AI;

namespace Onion_AI
{
    public class EnemyManager : CharacterManager
    {
        //In_Built Components
        public NavMeshAgent navMeshAgent {get; private set;}
        
        //Onion_AI Components
        public EnemyCombat enemyCombat {get; private set;}
        public EnemyMovement enemyMovement {get; private set;}
        public EnemyStatistic enemyStatistic {get; private set;}

        [field: Header("Files")]
        [HideInInspector] public Enemy_Data enemyData;
        [field: SerializeField] public WayPointConfig wayPointConfig {get; private set;}

        [field: Header("Status")]
        [field: SerializeField] public bool hasSetPath {get; private set;}

        protected override void Awake()
        {
            base.Awake();
            wayPointConfig = Instantiate(wayPointConfig);
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();

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

        public void Initialize(GameManager manager)
        {
            gameManager = manager;
            characterStatistics?.ResetHealth();
        }

        public void SetEnemyFirstPath(bool setPath)
        {
            hasSetPath = setPath;
        }
    }
}
