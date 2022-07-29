using System;
using Ingosstrakh.AnimationController.AnimationBroadcaster;
using UnityEngine;

namespace Ingosstrakh.AnimationController
{
    public class AnimationController : MonoBehaviour, IStateMachineExitReceiver, IStateMachineEnterReceiver
    {
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private AudioClip moveInClip;
        [SerializeField] private AudioClip moveOutClip;
        [SerializeField] private AudioSource audioSource;
        private void OnEnable()
        {
            playerAnimator.SetTrigger(AnimationState.MoveIn.ToString());
        }

        private void PlayAnimationRange(AnimationConfig.AnimationDescription animationDescription)
        {
        }

        void IStateMachineExitReceiver.OnAnimatorExitState(Animator animator, AnimatorStateInfo state)
        {
            if (state.IsTag(AnimationState.MoveIn.ToString()))
            {

            }
            else if (state.IsTag(AnimationState.Idle.ToString()))
            {

            }
            else if (state.IsTag(AnimationState.Stay.ToString()))
            {

            }
            else if (state.IsTag(AnimationState.MoveOut.ToString()))
            {
                playerAnimator.gameObject.SetActive(false);
                audioSource.clip = null;
            }
        }
        void IStateMachineEnterReceiver.OnAnimatorEnterState(Animator animator, AnimatorStateInfo state)
        {
            if (state.IsTag(AnimationState.MoveIn.ToString()))
            {
                audioSource.Stop();
                audioSource.clip = moveInClip;
                audioSource.Play();
            }
            else if (state.IsTag(AnimationState.Idle.ToString()))
            {

            }
            else if (state.IsTag(AnimationState.Stay.ToString()))
            {

            }
            else if (state.IsTag(AnimationState.MoveOut.ToString()))
            {
                audioSource.Stop();
                audioSource.clip = moveOutClip;
                audioSource.Play();
            }
        }
    }
}