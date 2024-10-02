using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    public class GameManager : MonoBehaviour
    {
        private int totalScore;

        public Boundaries[] boundaries {get; private set;}
        public LevelSpawners levelSpawners {get; private set;}
        public EnemySpawner enemySpawner {get; private set;}
        public EnvironmentManager environmentManager {get; private set;}

        [field: Header("Camaera Parameters")]
        public Vector2 maxCameraBounds {get; private set;}
        public Vector2 minCameraBounds {get; private set;}

        [field: Header("Game Rules")]
        public int targetsNeededToKill;
        public PlayerManager playerManager;
        public UIManager uIManager;
        public float scoreTimeMultiplier;
        [field: SerializeField] public int TotalScore {get; private set;}
        [field: SerializeField] public int CoinsCollected {get; private set;}

        [Header("Status")]
        public bool hasBeenSet;
        public bool canProceed;
        [field: SerializeField] public bool missionAccomplished {get; private set;}

        private void Awake()
        {
            uIManager = FindObjectOfType<UIManager>();
            levelSpawners = GetComponent<LevelSpawners>();
            playerManager = FindObjectOfType<PlayerManager>();
            boundaries = GetComponentsInChildren<Boundaries>();
            
            enemySpawner = FindObjectOfType<EnemySpawner>();
            environmentManager = FindObjectOfType<EnvironmentManager>();

            uIManager.gameManager = this;
            enemySpawner.gameManager = this;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            InitializeBoundaries();
            levelSpawners.Initialize();

            Camera mainCamera = Camera.main;
            targetsNeededToKill = enemySpawner.spawnQuantity;
            minCameraBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
            maxCameraBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
        }

        // Update is called once per frame
        void Update()
        {
            UpdateTargetsToKill();
            if(missionAccomplished)
            {
                return;
            }
            float delta = Time.deltaTime;

            CalculateTotalScore(delta);
            enemySpawner.EnemySpawn_Updater();
            environmentManager.EnvironmentManager_Updater(delta);
        }

        //Functionalities
        private void CalculateTotalScore(float delta)
        {
            if(playerManager.isDead)
            {
                return;
            }

            totalScore += Mathf.FloorToInt(delta * scoreTimeMultiplier);
            uIManager.DisplayTotalScore(totalScore);
        }

        public void UpdateTargetsToKill()
        {
            if(targetsNeededToKill <= 0)
            {
                hasBeenSet = false;
                SetTargetKillsForMission(enemySpawner.spawnQuantity);
            }
        }

        public void SetTargetKillsForMission(int numberOfTargets)
        {
            if(hasBeenSet == true)
            {
                return;
            }
            hasBeenSet = true;
            targetsNeededToKill = numberOfTargets;
        }

        private void InitializeBoundaries()
        {
            foreach(Boundaries boundary in boundaries)
            {
                boundary.Initialize(this);
            }
        }
    }
}
