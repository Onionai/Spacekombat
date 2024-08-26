using UnityEngine;

namespace Onion_AI
{
    public class PlayerInput : MonoBehaviour
    {
        //Components 
        Controls controls;
        PlayerManager playerManager;

        //Parameters
        private Vector2 movementInput;
        public float totalMoveAmount {get; private set;}
        public float verticalMoveAmount {get; private set;}
        public float horizontalMoveAmount {get; private set;}

        void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
        }

        private void OnEnable()
        {
            if(controls == null)
            {
                controls = new Controls();

                controls.Movement.Movement.performed += x => movementInput = x.ReadValue<Vector2>();
            }
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        // Update is called once per frame
        public void PlayerInput_Update()
        {
            HandleMovement();
        }

        //Functionalities
        private void HandleMovement()
        {
            verticalMoveAmount = movementInput.y;
            horizontalMoveAmount = movementInput.x;
            totalMoveAmount = Mathf.Clamp01(Mathf.Abs(verticalMoveAmount) + Mathf.Abs(horizontalMoveAmount));

            playerManager.isMoving = (totalMoveAmount > 0.01f);
        }
    }
}
