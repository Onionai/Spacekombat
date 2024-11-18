using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Onion_AI
{
    public class EnemyManagersController_Fixed : EnemyManagersController
    {
        //Formation Setter
        protected float delayTimer;
        protected bool hasSpawned30;
        protected int numberOfSpawnRounds;

        private FormationController formationController;
        public List<Vector3> formationPoints {get; private set;} = new();

        protected override void Awake()
        {
            base.Awake();
            formationController = GetComponent<FormationController>();
        }

        public override void InitializeController(GameManager GM)
        {
            base.InitializeController(GM);

            spawnRoundCount = 1;
            numberOfSpawnRounds = Random.Range(3,6);
        }

        public override void EnemyManagerController_Updater()
        {
            if(target.isDead)
            {
                missionStatus = MissionStatus.Completed;
                gameObject.SetActive(false);
                return;
            }
            
            if(hasSetSpawnQuantity && killedEnemies >= spawnQuantity)
            {
                if(delayTimer > 0.0f) delayTimer -= Time.deltaTime;

                if(delayTimer <= 0.0f)
                {
                    spawnRoundCount++;
                    if(spawnRoundCount > numberOfSpawnRounds)
                    {
                        missionStatus = MissionStatus.Failed;
                        return;
                    }

                    killedEnemies = 0;
                    hasSetSpawnQuantity = false; //Consider the Reset In Initialization for Refactoring
                    delayTimer = defaultDelayTimer;
                }
            }

            SpawnEnemies();
            formationPoints = formationController.EvaluatePoints().ToList();
            WaitTillEnemiesSet();
        }

        protected override void SpawnEnemies()
        {
            if(hasSetSpawnQuantity != true) 
            {
                numberOfSpawns = 0;
                PrepareSpawnPoints();
                spawnedEnemies.Clear();
                SetSpawnQuantityAndFormationParameters();
            }

            if(numberOfSpawns >= spawnQuantity)
            {
                return;
            }

            base.SpawnEnemies();
        }

        protected override void Spawn()
        {
            base.Spawn();
            for(int i = 0; i < spawnPoints.Count; i++)
            {
                SpawnFromSpawnPoint(i);
            }
        }

        protected override void SetSpawnQuantityAndFormationParameters()
        {
            int setFormationTypeRandomly = Random.Range(0,4);
            formationController.formationType = (setFormationTypeRandomly >= 3) ? FormationType.Box : FormationType.Circle;
            base.SetSpawnQuantityAndFormationParameters();
            
            int randomWidthSetter = (formationController.formationType == FormationType.Box) ? spawnQuantity : Random.Range(1,3);
            formationController.Initialize(randomWidthSetter, spawnQuantity);
        }

        private void WaitTillEnemiesSet()
        {
            if(numberOfSpawns < spawnQuantity)
            {
                return;
            }

            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                spawnPoint.RandomShooting();
            }
        }

        private void SpawnFromSpawnPoint(int i)
        {
            SpawnPoint spawnPoint = spawnPoints[i];
            spawnPoint.SpawnEnemyManager();
        }

        protected override int SpawnQuantity()
        {
            int random = (hasSpawned30 != true) ? Random.Range(0,8) : Random.Range(0,7);

            if(random >= 0 && random < 3)
            {
                if(formationController.formationType == FormationType.Circle)
                {
                    return 16;
                }
                return 18;
            }
            if(random >= 3 && random < 6)
            {
                if(formationController.formationType == FormationType.Circle)
                {
                    return 18;
                }
                return 24;
            }

            hasSpawned30 = true;
            if(formationController.formationType == FormationType.Circle)
            {
                return 20;
            }
            return 30;
        }

        protected override void PrepareSpawnPoints()
        {
            PathControllerClass pathControllerClass = pathController.RandomPathCreator_Fixed();

            for(int i = 0; i < spawnPoints.Count; i++)
            {
                SpawnPoint spawnPoint = spawnPoints[i];
                 
                spawnPoint.spawnedEnemies.Clear();
                spawnPoint.Initialize(pathControllerClass.pathControllers[i]);
            }
        }
        public Vector3 SpawnHolderPosition()
        {
            if(formationController.formationType == FormationType.Box)
            {
                return spawnPointHolder.position;
            }
            return new Vector3(-0.4f, spawnPointHolder.position.y + 0.2f, 0);
        }
    }
}
