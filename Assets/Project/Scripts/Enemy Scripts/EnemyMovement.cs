using UnityEngine;

namespace Onion_AI
{
    public class EnemyMovement : CharacterMovement
    {
        protected EnemyManager enemyManager;

        protected override void Awake()
        {
            base.Awake();
            enemyManager = characterManager as EnemyManager;
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void CharacterMovement_FixedUpdate(float delta)
        {
            base.CharacterMovement_FixedUpdate(delta);
        }

        //Functionalities 

        protected override void HandleMovement(float delta)
        {
            
        }

        protected override void HandleRotation(float delta)
        {

        }
    }
}
