using UnityEngine;

namespace Onion_AI
{
    public class CharacterDamageCollider : MonoBehaviour
    {
        private float damageValue;
        public float damageModifier;
        public CharacterManager characterCausingDamage;

        public void OnBulletColliderHit(WeaponManager weaponManager, CharacterManager characterDamaged)
        {
            if(characterDamaged.isDead)
            {
                return;
            }

            if(characterDamaged.characterType == characterCausingDamage.characterType)
            {
                return;
            }
            
            //Particle Effect
            damageValue = characterCausingDamage.characterCombat.damageModifier * damageModifier;
            characterDamaged.characterStatistics.TakeDamage(damageValue);
            weaponManager.ReleaseFromPool();
        }
    }
}
