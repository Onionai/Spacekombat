using UnityEngine;

namespace Onion_AI
{
    public class WeaponManager : MonoBehaviour, IReleaseFromPool
    {
        //Manager
        private WeaponData weaponData;
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

        public void SetWeaponData(WeaponData wd)
        {
            weaponData = wd;
        }

        public void Initialize(Transform spawnPoint, CharacterCombat characterCombat)
        {
            characterManager = characterCombat.characterManager;
            characterDamageCollider.characterCausingDamage = characterManager;

            transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            rigidBody.AddForce(spawnPoint.up * projectileSpeed * EnvironmentManager.gameSpeedMultiplier, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Bullet") || other.CompareTag("Shield"))
            {
                ReleaseFromPool();
                return;
            }

            CharacterManager damagedCharacter = other.GetComponentInParent<CharacterManager>();
            if(damagedCharacter != null)
            {
                characterDamageCollider.OnBulletColliderHit(other, levelSpawner, damagedCharacter);

                if(damagedCharacter.characterType != characterManager.characterType)
                {
                    ReleaseFromPool();
                }
            }
        }

        public void ReleaseFromPool()
        {
            weaponData.ReleaseObject(this);
        }
    }
}
