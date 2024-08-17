using UnityEngine;

namespace Onion_AI
{
    public class CharacterCombat : MonoBehaviour
    {
        //Manager
        public CharacterManager characterManager {get; private set;}

        //Parameters
        [SerializeField] protected Transform[] firePoints;

        // Update is called once per frame
        public virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        public virtual void CharacterCombat_Update()
        {
        
        }

        protected virtual void Shoot()
        {
            
        }

        protected virtual void Fire(Transform firePoint)
        {
            
        }
    }
}
