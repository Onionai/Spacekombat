using UnityEngine;

namespace Onion_AI
{
    public class PlayerManager : CharacterManager
    {
        //Onion_AI Components
        public GameManager gameManager {get; private set;}
        public PlayerInput playerInput {get; private set;}
        public PlayerCombat playerCombat {get; private set;}
        public PlayerMovement playerMovement {get; private set;}
        public PlayerStatistic playerStatistic {get; private set;}

        protected override void Awake()
        {
            base.Awake();
            playerInput = GetComponent<PlayerInput>();
            gameManager = FindObjectOfType<GameManager>();

            playerCombat = characterCombat as PlayerCombat;
            playerMovement = characterMovement as PlayerMovement;
            playerStatistic = characterStatistics as PlayerStatistic;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            playerInput.PlayerInput_Update();

            base.Update();
        }
    }
}
