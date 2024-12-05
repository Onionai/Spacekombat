using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    public class CharacterCombat : MonoBehaviour
    {
        //Manager
        protected float deltaTime;
        public CharacterManager characterManager {get; private set;}

        [Header("Parameters")]
        [SerializeField] protected float fireRate;
        [field: SerializeField] public List<Transform> firePoints {get; protected set;} = new();

        [Header("Damage Modifiers")]
        public float currentDamageModifier;
        [field: SerializeField] public float damageModifier { get; protected set; }

        public virtual void Awake()
        {
            GetFirePoints();
            characterManager = GetComponentInParent<CharacterManager>();
        }

        protected virtual void Start()
        {
            currentDamageModifier = damageModifier;
        }

        public virtual void CharacterCombat_Update(float delta)
        {
            Shoot(delta);
        }

        protected virtual void Shoot(float delta)
        {
            foreach(Transform firePoint in firePoints)
            {
                Fire(firePoint);
            }
            
            if(characterManager.characterAnimationManager != null) 
            {
                characterManager.characterAnimationManager.PlayTargetAnimation(characterManager.characterAnimationManager.shootingHash, false);
            }
        }

        private void GetFirePoints()
        {
            for(int index = 0; index < transform.childCount; index++)
            {
                Transform firePoint = transform.GetChild(index);
                firePoints.Add(firePoint);
            }
        }

        protected virtual void Fire(Transform firePoint)
        {
            WeaponManager fireObject = characterManager.gameManager.levelSpawners.RandomBulletShooter();
            
            fireObject.Initialize(firePoint, this);
            fireObject.levelSpawner = characterManager.gameManager.levelSpawners;
        }
    }
}
