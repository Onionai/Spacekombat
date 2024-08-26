using UnityEngine;

namespace Onion_AI
{
    public class GameManager : MonoBehaviour
    {
        public Boundaries[] boundaries {get; private set;}
        public EnemySpawner enemySpawner {get; private set;}
        public LevelSpawners levelSpawners {get; private set;}
        //public EnvironmentManager environmentManager {get; private set;}

        [field: Header("Game Rules")]
        [field: SerializeField] public int TotalScore {get; private set;}
        [field: SerializeField] public int CoinsCollected {get; private set;}
        [field: SerializeField] public int TargetsNeededToKill {get; private set;}
        [field: SerializeField] public bool missionAccomplished {get; private set;}

        private void Awake()
        {
            levelSpawners = GetComponent<LevelSpawners>();
            enemySpawner = FindObjectOfType<EnemySpawner>();
            boundaries = GetComponentsInChildren<Boundaries>();
            //environmentManager = FindObjectOfType<EnvironmentManager>();
        }
        
        // Start is called before the first frame update
        void Start()
        {
            InitializeBoundaries();
            levelSpawners.Initialize();
            enemySpawner.gameManager = this;
            SetTargetKillsForMission(enemySpawner.spawnQuantity);
        }

        // Update is called once per frame
        void Update()
        {
            missionAccomplished = TargetsNeededToKill <= 0;

            if(missionAccomplished)
            {
                return;
            }
            float delta = Time.deltaTime;
            enemySpawner.EnemySpawn_Updater();
            //environmentManager.EnvironmentManager_Updater(delta);
        }

        //Functionalities

        private void InitializeBoundaries()
        {
            foreach(Boundaries boundary in boundaries)
            {
                boundary.Initialize(this);
            }
        }

        public void KilledTarget()
        {
            TargetsNeededToKill -= 1;
        }

        public void SetTargetKillsForMission(int numberOfTargets)
        {
            TargetsNeededToKill = numberOfTargets;
        }
    }
}
