using UnityEngine;
using UnityEngine.InputSystem;

namespace Onion_AI
{
    public class PlayerInput : MonoBehaviour
    {
        //Components 
        Controls controls;
        Camera mainCamera;
        PlayerManager playerManager;

        //Input value
        private bool isDragging;
        private Vector2 swipeMovementInput;
        private Vector2 touchMovementInput;

        //Parameters
        private float totalMoveAmount;
        public float verticalMoveAmount {get; private set;}
        public float horizontalMoveAmount {get; private set;}

        void Awake()
        {
            mainCamera = Camera.main;
            playerManager = GetComponent<PlayerManager>();
        }

        private void OnEnable()
        {
            if(controls == null)
            {
                controls = new Controls();

                controls.Movement.Drag.performed += x => isDragging = true;
                controls.Movement.Drag.canceled += x => isDragging = false;

                controls.Movement.DeltaMovement.performed += x => swipeMovementInput = x.ReadValue<Vector2>();
                controls.Movement.TouchMovement.performed += x => touchMovementInput = x.ReadValue<Vector2>();
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

        public static bool IsCurrentDeviceMouse()
        {
            //Check if there is an active mouse or touchscreen
            if((Mouse.current != null && Mouse.current.enabled && Mouse.current.delta.ReadValue() != Vector2.zero)
                    || (Touchscreen.current != null && Touchscreen.current.enabled && Touchscreen.current.delta.ReadValue() != Vector2.zero))
            {
                return true;
            }
            return false;
        }

        //Functionalities
        private void HandleMovement()
        {
            if(IsCurrentDeviceMouse())
            {
                playerManager.isMoving = isDragging;
                verticalMoveAmount = touchMovementInput.y;
                horizontalMoveAmount = touchMovementInput.x;
                return;
            }

            verticalMoveAmount = swipeMovementInput.y;
            horizontalMoveAmount = swipeMovementInput.x;
            totalMoveAmount = Mathf.Clamp01(Mathf.Abs(verticalMoveAmount) + Mathf.Abs(horizontalMoveAmount));
            playerManager.isMoving = totalMoveAmount > 0.01f;
        }
    }
}
