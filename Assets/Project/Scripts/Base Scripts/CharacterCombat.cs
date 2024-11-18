using System.Collections.Generic;
using UnityEngine;

namespace Onion_AI
{
    public class CharacterCombat : MonoBehaviour
    {
        //Manager
        public CharacterManager characterManager {get; private set;}
        protected float deltaTime;

        [Header("Parameters")]
        [SerializeField] protected float fireRate;
        [field: SerializeField] public float damageModifier {get; private set;}
        [field: SerializeField] public List<Transform> firePoints {get; protected set;} = new();

        public virtual void Awake()
        {
            GetFirePoints();
            characterManager = GetComponentInParent<CharacterManager>();
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
