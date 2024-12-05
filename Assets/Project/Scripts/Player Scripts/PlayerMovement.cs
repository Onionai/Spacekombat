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

        private Vector2 TargetPosition(float delta)
        {
            Vector2 movePosition = targetPosition = acceleration * movementSpeed * EnvironmentManager.gameSpeedMultiplier * moveDirection;
            return delta * movePosition;
        }

        protected override void HandleMovement(float delta)
        {
            bool isTouch = PlayerInput.IsCurrentDeviceMouse();

            if(isTouch)
            {
                HandleTouchMovement(delta);
                return;
            }
            HandleDeltaMovement(delta);
        }

        // Touch or Mouse Input, Enables Dragging
        private void HandleTouchMovement(float delta)
        {
            if(playerManager.isMoving == false)
            {
                return;
            }

            Vector3 touchPosition = new Vector3(playerManager.playerInput.horizontalMoveAmount, playerManager.playerInput.verticalMoveAmount, 0f);
            targetPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            transform.position = Vector3.Lerp(transform.position, targetPosition, delta * movementSpeed);
        }

        //Gamepad or Keyboard Input, Enables Swiping
        private void HandleDeltaMovement(float delta)
        {
            if (moveDirection == Vector2.zero)
            {
                return;
            }

            targetPosition = TargetPosition(delta) + playerManager.rigidBody.position;

            Vector2 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            playerManager.rigidBody.MovePosition(smoothPosition);
        }
    }
}
