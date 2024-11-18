using UnityEngine;

namespace Onion_AI
{
    public class CharacterDamageCollider : MonoBehaviour
    {
        private float damageValue;
        public float damageModifier;
        public CharacterManager characterCausingDamage;

        public void OnBulletColliderHit(Collider2D other, LevelSpawners levelSpawners, CharacterManager characterDamaged)
        {
            if(characterDamaged.isDead)
            {
                return;
            }

            if(characterDamaged.characterType == characterCausingDamage.characterType)
            {
                return;
            }
            
            characterDamaged.characterStatistics.TakeDamage(damageValue);
            Vector2 contactPoint = other.ClosestPoint(transform.position);
            damageValue = characterCausingDamage.characterCombat.damageModifier * damageModifier;
            LevelSpawners.RandomParticleEffect(contactPoint, Quaternion.identity, levelSpawners.impactFXArray);
        }
    }
}
