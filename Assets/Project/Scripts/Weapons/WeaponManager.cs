using UnityEngine;

namespace Onion_AI
{
    public class WeaponManager : MonoBehaviour
    {
        //Manager
        [HideInInspector] public CharacterManager characterManager;

        //Components
        public Rigidbody2D rigidBody {get; private set;}
        [HideInInspector] public LevelSpawners levelSpawner;
        private CharacterDamageCollider characterDamageCollider;

        [Header("Stats")]
        [SerializeField] public float projectileSpeed;

        private void OnEnable()
        {
            if(rigidBody == null) rigidBody = GetComponent<Rigidbody2D>();
            if(characterDamageCollider == null) characterDamageCollider = GetComponent<CharacterDamageCollider>();
        }

        public void Initialize(Transform spawnPoint, CharacterCombat characterCombat)
        {
            characterManager = characterCombat.characterManager;
            transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

            rigidBody.AddForce(spawnPoint.up * projectileSpeed, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            characterDamageCollider.OnColliderHit();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            characterDamageCollider.OnColliderExit();
        }
    }
}
