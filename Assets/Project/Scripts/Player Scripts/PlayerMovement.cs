using UnityEngine;

namespace Onion_AI
{
    public class PlayerMovement : CharacterMovement
    {
        protected PlayerManager playerManager;
        private Vector2 targetPosition;
        private float smoothTime = 0.5f;
        private Vector3 velocity = Vector3.zero;

        protected override void Awake()
        {
            base.Awake();
            playerManager = characterManager as PlayerManager;
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void CharacterMovement_Update(float delta)
        {
            if(playerManager.performingAction)
            {
                return;
            }
            moveDirection = playerManager.playerInput.verticalMoveAmount * cameraObject.up;
            moveDirection += (Vector2) (playerManager.playerInput.horizontalMoveAmount * cameraObject.right);
            moveDirection.Normalize();
        }

        public override void CharacterMovement_FixedUpdate(float delta)
        {
            if(playerManager.performingAction)
            {
                return;
            }
            HandleMovement(delta);
            base.CharacterMovement_FixedUpdate(delta);
        }

        //Functionalities

        public void ClampPlayerPosition()
        {
            playerManager.transform.localPosition = ClampedMovement(playerManager.transform.localPosition);
        }

        protected override void HandleMovement(float delta)
        {
            if(moveDirection != Vector2.zero)
            {
                targetPosition = acceleration * movementSpeed * EnvironmentManager.gameSpeedMultiplier * delta * moveDirection + playerManager.rigidBody.position;

                Vector2 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
                playerManager.rigidBody.MovePosition(smoothPosition);
            }
        }
    }
}
