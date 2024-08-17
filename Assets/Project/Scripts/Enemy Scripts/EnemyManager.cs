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

        protected override void Awake()
        {
            base.Awake();
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
    }
}
