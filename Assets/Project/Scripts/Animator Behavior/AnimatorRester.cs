using UnityEngine;

namespace Onion_AI
{
    public class AnimatorRester : StateMachineBehaviour
    {
        CharacterAnimationManager characterAnimationManager;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(characterAnimationManager == null)
            {
                characterAnimationManager = animator.GetComponent<CharacterAnimationManager>();
            }
            animator.SetBool(characterAnimationManager.performActionHash, false);
        }
    }
}