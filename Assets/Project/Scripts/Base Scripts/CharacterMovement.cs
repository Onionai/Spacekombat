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
            GameManager game = GameManager.Instance;
            Vector2 minCameraBounds = game.minCameraBounds;
            Vector2 maxCameraBounds = game.maxCameraBounds;

            float xPos = Mathf.Clamp(position.x, minCameraBounds.x + leftPadding, maxCameraBounds.x - rightPadding);
            float yPos = Mathf.Clamp(position.y, minCameraBounds.y + bottomPadding, maxCameraBounds.y - topPadding);

            Vector3 clampedPosition = new(xPos, yPos, position.z);
            return clampedPosition;
        }

        protected virtual void HandleMovement(float delta)
        {

        }
    }
}
