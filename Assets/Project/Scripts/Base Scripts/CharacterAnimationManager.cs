using UnityEngine;
using System.Collections.Generic;

namespace Onion_AI
{
    public class CharacterAnimationManager : MonoBehaviour
    {
        private CharacterManager characterManager;

        [Header("Animation State Hash Codes")]
        public int spawnHash;
        public int deathHash;
        public int reloadHash;
        public int shootingHash;

        [Header("Animation Parameter Hash Codes")]
        public int performActionHash;

        private void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        public void SetHashParameters()
        {
            spawnHash = Animator.StringToHash("Spawn");
            deathHash = Animator.StringToHash("Death");
            reloadHash = Animator.StringToHash("Reload");
            shootingHash = Animator.StringToHash("Shooting");

            performActionHash = Animator.StringToHash("performAction");
        }

        public void PlayTargetAnimation(int targetAnimation, bool performAction, float transitionDuration = 0.15f)
        {
            characterManager.animator.SetBool(performActionHash, performAction);
            characterManager.animator.CrossFade(targetAnimation, transitionDuration);
        }
    }
}
