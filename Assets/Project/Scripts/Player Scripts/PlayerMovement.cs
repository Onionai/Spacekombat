using UnityEngine;

namespace Onion_AI
{
    public class PlayerMovement : CharacterMovement
    {
        protected PlayerManager playerManager;

        protected override void Awake()
        {
            base.Awake();
            playerManager = characterManager as PlayerManager;
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void CharacterMovement_FixedUpdate(float delta)
        {
            HandleMovement(delta);
            base.CharacterMovement_FixedUpdate(delta);
        }

        //Functionalities

        private void ClampMovement()
        {
            
        }

        protected override void HandleMovement(float delta)
        {
            moveDirection = playerManager.playerInput.verticalMoveAmount * cameraObject.up;
            moveDirection += playerManager.playerInput.horizontalMoveAmount * cameraObject.right;
            moveDirection.Normalize();

            moveDirection *= acceleration * movementSpeed * delta;
            playerManager.rigidBody.velocity = moveDirection;
        }

        protected override void HandleRotation(float delta)
        {
            
        }
    }
}
