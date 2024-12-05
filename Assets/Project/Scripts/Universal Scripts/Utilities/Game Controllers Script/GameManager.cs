using UnityEngine;

namespace Onion_AI
{
    public class GameManager : MonoBehaviour
    {
        public static int totalScore;

        public AudioManager audioManager {get; private set;}
        public Boundaries[] boundaries {get; private set;}
        public EnemySpawner enemySpawner {get; private set;}
        public LevelSpawners levelSpawners {get; private set;}
        public EnvironmentManager environmentManager {get; private set;}

        [field: Header("Camera Parameters")]
        public Vector2 maxCameraBounds {get; private set;}
        public Vector2 minCameraBounds {get; private set;}

        [field: Header("Public Components")]
        public UIManager uIManager;
        private LoadPlayer loadPlayer;
        public PlayerManager playerManager;
        public static Transform playerTransform;
        [SerializeField] private HealthCounterPanel healthCounterPanel;

        [field: Header("Game Rules")]
        public int targetsNeededToKill;
        public float scoreTimeMultiplier;
        public PowerUpClass[] powerUpClassArray;
        [SerializeField] private Transform spawnPointPU;

        [Header("Status")]
        public bool hasBeenSet;
        public static GameState gameState = GameState.Active;
        [field: SerializeField] public bool missionAccomplished {get; private set;}

        private void Awake()
        {
            uIManager = FindObjectOfType<UIManager>();
            levelSpawners = GetComponent<LevelSpawners>();

            loadPlayer = FindObjectOfType<LoadPlayer>();
            audioManager = FindObjectOfType<AudioManager>();

            loadPlayer.LoadSelectedJet();
            playerManager = FindObjectOfType<PlayerManager>();
            
            playerTransform = playerManager.transform;
            boundaries = GetComponentsInChildren<Boundaries>();
            
            enemySpawner = FindObjectOfType<EnemySpawner>();
            environmentManager = FindObjectOfType<EnvironmentManager>();

            uIManager.gameManager = this;
            enemySpawner.gameManager = this;
            HealthCounterManager.Instance?.SetHealthCounterPanel(healthCounterPanel);
        }
        
        // Start is called before the first frame update
        void Start()
        {
            InitializeBoundaries();
            levelSpawners.Initialize();

            audioManager.PlaySound(101);
            Camera mainCamera = Camera.main;
            targetsNeededToKill = enemySpawner.spawnQuantity;
            minCameraBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
            maxCameraBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
        }

        // Update is called once per frame
        void Update()
        {
            PlayGamePlaySound();
            if(gameState == GameState.Paused)
            {
                return;
            }

            if(gameState == GameState.Resume)
            {
                uIManager.HandlePauseCountdown();
                return;
            }
            
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

        public void SpawnPoweerUp()
        {
            int random = Random.Range(0, powerUpClassArray.Length);
            PowerUpClass powerUpClass = Instantiate(powerUpClassArray[random], spawnPointPU);
        }

        private void PlayGamePlaySound()
        {
            if(playerManager.isDead)
            {
                audioManager.StopSound(101);
                audioManager.PlaySound(100);
                return;
            }
        }

        //Functionalities
        private void CalculateTotalScore(float delta)
        {
            if(playerManager.isDead)
            {
                return;
            }

            float currentWaveFactor = (EnemySpawner.waveCount - 1)/10f;
            totalScore += Mathf.FloorToInt(delta * scoreTimeMultiplier * currentWaveFactor);
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
