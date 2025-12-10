using UnityEngine;

namespace Onion_AI
{
    public class BossManager : CharacterManager
    {
        // Start is called before the first frame update
        public int appearanceCount {get; private set;}

        //Boss Components
        public BossStats bossStats {get; private set;}

        protected override void Awake()
        {
            base.Awake();
            bossStats = characterStatistics as BossStats;
        }

        protected override void Start()
        {
            bossStats.ResetHealth();
            characterAnimationManager.SetHashParameters();
            characterAnimationManager.PlayTargetAnimation(characterAnimationManager.spawnHash, true);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public void IncreaseAppearanceCount()
        {
            appearanceCount++;
        }

        protected override void Update()
        {
            if (GameManager.Instance.CompareGameStatus(GamePlayState.Active) != true)
            {
                return;
            }
            base.Update();
        }
    }
}
