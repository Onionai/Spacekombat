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
            if (characterDamaged.isDead)
            {
                return;
            }

            if(characterDamaged.characterType == characterCausingDamage.characterType)
            {
                return;
            }

            damageValue = characterCausingDamage.characterCombat.currentDamageModifier * damageModifier;

            characterDamaged.characterStatistics.TakeDamage(damageValue);
            Vector2 contactPoint = other.ClosestPoint(transform.position);
            LevelSpawners.RandomParticleEffect(contactPoint, Quaternion.identity, levelSpawners.impactFXArray);
        }
    }
}
