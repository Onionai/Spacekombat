using UnityEngine;
using System.Collections;

namespace Onion_AI
{
    public class BossCombat : CharacterCombat
    {
        private Coroutine attackRoutine;
        public BossManager bossManager {get; private set;}
        
        private WaitForSeconds delayShotTimer;
        private WaitForSeconds shootTimePeriod;
        private BossFiringData currentFiringState;

        [Header("Delay Timer")]
        [SerializeField] private float shootTimer;
        [SerializeField] private float delayTimer;
        [SerializeField] private BossFiringData[] boss_FiringState;

        public override void Awake()
        {
            base.Awake();
            
            bossManager = characterManager as BossManager;

            delayShotTimer = new WaitForSeconds(delayTimer);
            shootTimePeriod = new WaitForSeconds(shootTimer);

            for(int i = 0; i < boss_FiringState.Length; i++)
            {
                BossFiringData firingState = Instantiate(boss_FiringState[i]);

                boss_FiringState[i] = firingState;
                firingState.Initialize(this);
            }
        }

        private void Start()
        {
            int random = Random.Range(0, boss_FiringState.Length);
            currentFiringState = boss_FiringState[random];

            StartCoroutine(EnableShooting());
        }

        public override void CharacterCombat_Update(float delta)
        {
            attackRoutine = StartCoroutine(StartShooting());
        }

        private IEnumerator EnableShooting()
        {
            yield return delayShotTimer;

            if(bossManager.canShoot != true)
            {
                bossManager.canShoot = true;
            }
        }

        private IEnumerator StartShooting()
        {
            currentFiringState.HandleFiring(Time.deltaTime, fireRate);
            
            yield return shootTimePeriod;

            bossManager.canShoot = false;
            int random = Random.Range(0, boss_FiringState.Length);
            currentFiringState = boss_FiringState[random];
            StartCoroutine(EnableShooting());
        }
    }
}
