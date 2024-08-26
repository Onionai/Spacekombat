using UnityEngine;

namespace Onion_AI
{
    public class WeaponManager : MonoBehaviour, IReleaseFromPool
    {
        //Manager
        [HideInInspector] public CharacterManager characterManager;

        //Components
        public Rigidbody2D rigidBody {get; private set;}
         public LevelSpawners levelSpawner;
        private CharacterDamageCollider characterDamageCollider;

        [Header("Stats")]
        [SerializeField] public float projectileSpeed;

        private void OnEnable()
        {
            if(rigidBody == null) rigidBody = GetComponent<Rigidbody2D>();
            if(characterDamageCollider == null)
            {
                characterDamageCollider = GetComponent<CharacterDamageCollider>();
            }
        }

        public void Initialize(Transform spawnPoint, CharacterCombat characterCombat)
        {
            characterManager = characterCombat.characterManager;
            characterDamageCollider.characterCausingDamage = characterManager;
            transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

            rigidBody.AddForce(spawnPoint.up * projectileSpeed, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            CharacterManager damagedCharacter = other.GetComponentInParent<CharacterManager>();

            if(damagedCharacter != null)
            {
                characterDamageCollider.OnColliderHit(damagedCharacter);
            }
        }

        public void ReleaseFromPool()
        {
            levelSpawner.bulletPool.Release(this);
        }
    }
}
