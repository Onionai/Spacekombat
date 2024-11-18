using UnityEngine;

namespace Onion_AI
{
    public class CharacterMovement : MonoBehaviour
    {
        #region Private Parameters

        //Component
        protected CharacterManager characterManager;
        
        //Movements
        public Vector2 moveDirection;
        #endregion

        [field: Header("Parameters")]
        [field: SerializeField] public Camera mainCamera {get; private set;}
        [field: SerializeField] public Transform cameraObject {get; private set;}

        [field: Header("Player Stats")]
        [field: SerializeField] public float acceleration {get; private set;} = 10;
        [field: SerializeField] public float movementSpeed {get; private set;} = 100;

        [field: Header("Clamped Position")]
        [field: SerializeField] public float topPadding {get; private set;} = -12.55f;
        [field: SerializeField] public float leftPadding {get; private set;} = -22.5f;
        [field: SerializeField] public float rightPadding {get; private set;} = -22.5f;
        [field: SerializeField] public float bottomPadding {get; private set;} = 5.0f;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            mainCamera = Camera.main;
            cameraObject = mainCamera.transform;
        }

        public virtual void CharacterMovement_Update(float delta)
        {

        }

        public virtual void CharacterMovement_FixedUpdate(float delta)
        {
            
        }

        //Functionalities 

        protected Vector3 ClampedMovement(Vector3 position)
        {
            float xPos = Mathf.Clamp(position.x, characterManager.gameManager.minCameraBounds.x + leftPadding, characterManager.gameManager.maxCameraBounds.x - rightPadding);
            float yPos = Mathf.Clamp(position.y, characterManager.gameManager.minCameraBounds.y + bottomPadding, characterManager.gameManager.maxCameraBounds.y - topPadding);

            Vector3 clampedPosition = new Vector3(xPos, yPos, position.z);
            return clampedPosition;
        }

        protected virtual void HandleMovement(float delta)
        {

        }
    }
}
