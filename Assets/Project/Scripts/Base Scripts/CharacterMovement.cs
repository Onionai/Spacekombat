using UnityEngine;

namespace Onion_AI
{
    public class CharacterMovement : MonoBehaviour
    {
        #region Private Parameters

        //Component
        protected CharacterManager characterManager;

        //ClampMovement
        protected Vector2 maxCameraBounds;
        protected Vector2 minCameraBounds;
        
        //Movements
        protected Vector3 moveDirection;
        protected Vector3 rotateDirection;

        #endregion

        [field: Header("Parameters")]
        [field: SerializeField] public Camera mainCamera {get; private set;}
        [field: SerializeField] public Transform cameraObject {get; private set;}

        [field: Header("Player Stats")]
        [field: SerializeField] public float acceleration {get; private set;} = 10;
        [field: SerializeField] public float movementSpeed {get; private set;} = 100;
        [field: SerializeField] public float rotationSpeed {get; private set;} = 20.0f;

        [field: Header("Clamped Position")]
        [field: SerializeField] public float topPadding {get; private set;} = -12.55f;
        [field: SerializeField] public float leftPadding {get; private set;} = -22.5f;
        [field: SerializeField] public float rightPadding {get; private set;} = -22.5f;
        [field: SerializeField] public float bottomPadding {get; private set;} = 5.0f;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void OnEnable()
        {
            
        }

        protected virtual void Start()
        {
            mainCamera = Camera.main;
            cameraObject = mainCamera.transform;

            minCameraBounds = mainCamera.ViewportToWorldPoint(new Vector2(0,0));
            maxCameraBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
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
            float xPos = Mathf.Clamp(position.x, minCameraBounds.x + leftPadding, maxCameraBounds.x - rightPadding);
            float yPos = Mathf.Clamp(position.y, minCameraBounds.y + bottomPadding, maxCameraBounds.y - topPadding);

            Vector3 clampedPosition = new Vector3(xPos, yPos, position.z);
            return clampedPosition;
        }

        protected virtual void HandleMovement(float delta)
        {

        }

        protected virtual void HandleRotation(float delta)
        {

        }
    }
}
