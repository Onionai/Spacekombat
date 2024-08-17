using UnityEngine;

namespace Onion_AI
{
    public class GameManager : MonoBehaviour
    {
        public EnemySpawner enemySpawner {get; private set;}
        public LevelSpawners levelSpawners {get; private set;}
        public EnvironmentManager environmentManager {get; private set;}

        private void Awake()
        {
            enemySpawner = GetComponent<EnemySpawner>();
            levelSpawners = GetComponent<LevelSpawners>();

            environmentManager = FindObjectOfType<EnvironmentManager>();
        }
        
        // Start is called before the first frame update
        void Start()
        {
            levelSpawners.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;

            environmentManager.EnvironmentManager_Updater(delta);
        }
    }
}
