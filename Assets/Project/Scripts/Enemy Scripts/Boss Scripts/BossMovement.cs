using UnityEngine;

namespace Onion_AI
{
    public class BossMovement : CharacterMovement
    {
        protected BossManager bossManager;

        private float verticalAmount;
        private float distanceToTarget;
        private float horizontalAmount;

        protected override void Awake()
        {
            base.Awake();
            bossManager = characterManager as BossManager;
        }

        protected override void Start()
        {
            base.Start();
            PickRandomPosition();
        }

        public override void CharacterMovement_Update(float delta)
        {
            if(bossManager.performingAction)
            {
                return;
            }
        }

        public override void CharacterMovement_FixedUpdate(float delta)
        {
            if(bossManager.performingAction || bossManager.bossStats.runAway)
            {
                return;
            }
            
            HandleMovement(delta);
            base.CharacterMovement_FixedUpdate(delta);
        }

        //Functionalities

        protected override void HandleMovement(float delta)
        {
            float speed = acceleration * movementSpeed * delta;
            transform.position = Vector3.MoveTowards(transform.position, moveDirection, speed);

            distanceToTarget = Vector3.Distance(transform.position, moveDirection);
            if(distanceToTarget <= 0.25f)
            {
                Invoke(nameof(PickRandomPosition), 3f);
            }
        }

        private void PickRandomPosition()
        {
            verticalAmount = Random.Range(topPadding, bottomPadding);
            horizontalAmount = Random.Range(leftPadding, rightPadding);

            moveDirection = new Vector2(horizontalAmount, verticalAmount);
        }
    }
}
