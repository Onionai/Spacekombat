using UnityEngine;

namespace Onion_AI
{
    public class CharacterMovement : MonoBehaviour
    {
        //Component
        protected CharacterManager characterManager;

        [field: Header("Parameters")]
        [SerializeField] protected Vector3 moveDirection;
        protected Vector2 rotateDirection;
        [field: SerializeField] public Camera mainCamera {get; private set;}
        [field: SerializeField] public Transform cameraObject {get; private set;}

        [field: Header("Player Stats")]
        [field: SerializeField] public float acceleration {get; private set;}
        [field: SerializeField] public float movementSpeed {get; private set;}
        [field: SerializeField] public float rotationSpeed {get; private set;}

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            mainCamera = Camera.main;
            cameraObject = mainCamera.transform;
        }

        public virtual void CharacterMovement_FixedUpdate(float delta)
        {
            
        }

        //Functionalities 

        protected virtual void HandleMovement(float delta)
        {

        }

        protected virtual void HandleRotation(float delta)
        {

        }
    }
}
