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

        public override void CharacterMovement_Update(float delta)
        {
            
        }

        public override void CharacterMovement_FixedUpdate(float delta)
        {
            HandleMovement(delta);
            base.CharacterMovement_FixedUpdate(delta);
        }

        //Functionalities

        protected override void HandleMovement(float delta)
        {
            moveDirection = playerManager.playerInput.verticalMoveAmount * cameraObject.up;
            moveDirection += playerManager.playerInput.horizontalMoveAmount * cameraObject.right;
            moveDirection.Normalize();

            moveDirection *= acceleration * movementSpeed * delta;
            playerManager.rigidBody.velocity = moveDirection;
            playerManager.transform.localPosition = ClampedMovement(playerManager.transform.localPosition);
        }

        protected override void HandleRotation(float delta)
        {
            
        }
    }
}
