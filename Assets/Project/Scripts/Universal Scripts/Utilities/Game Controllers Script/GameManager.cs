using UnityEngine;

namespace Onion_AI
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public AudioManager Audio {get; private set;}
        public LevelSpawners Level { get; private set; }
        public EnemySpawner EnemySpawn {get; private set;}
        public EnvironmentManager Enviroment {get; private set;}

        [field: Header("Camera Parameters")]
        public Vector2 maxCameraBounds {get; private set;}
        public Vector2 minCameraBounds {get; private set;}

        [Header("Public Components")]
        public UIManager uiManager;
        private LoadPlayer loadPlayer;
        public PlayerManager playerManager;

        [Header("Game Rules")]
        public int targetsNeededToKill;
        public PowerUpClass[] powerUpClassArray;
        [SerializeField] private GameplayController gamePlayController;

        [Header("Status")]
        public bool hasBeenSet;

        public bool PlayerIsDead => playerManager.isDead;
        public GameplayController Controller => gamePlayController;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            gamePlayController = new();
            Level = GetComponent<LevelSpawners>();
            uiManager = FindObjectOfType<UIManager>();

            Audio = FindObjectOfType<AudioManager>();
            loadPlayer = FindObjectOfType<LoadPlayer>();

            loadPlayer.LoadSelectedJet();
            playerManager = FindObjectOfType<PlayerManager>();
            
            EnemySpawn = FindObjectOfType<EnemySpawner>();
            Enviroment = FindObjectOfType<EnvironmentManager>();
        }
        
        // Start is called before the first frame update
        void Start()
        {
            Level.Initialize();

            Audio.PlaySound(101);
            Camera mainCamera = Camera.main;
            minCameraBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
            maxCameraBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
        }

        // Update is called once per frame
        void Update()
        {
            PlayGamePlaySound();
            if(CompareGameStatus(GamePlayState.PlayerPause) || CompareGameStatus(GamePlayState.PlayerResume))
            {
                return;
            }
            float delta = Time.deltaTime;
            Enviroment.EnvironmentManager_Updater(delta);
        }

        public void SpawnPowerUp(Vector3 spawnPosition)
        {
            int random = Random.Range(0, powerUpClassArray.Length);
            Instantiate(powerUpClassArray[random], spawnPosition, Quaternion.identity);
        }

        private void PlayGamePlaySound()
        {
            if(playerManager.isDead)
            {
                Audio.StopSound(101);
                Audio.PlaySound(100);
                return;
            }
        }

        //Functionalities

        public bool CompareGameStatus(GamePlayState gamePlayState)
        {
            return gamePlayController.CurrentState.Equals(gamePlayState);
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
    }
}
