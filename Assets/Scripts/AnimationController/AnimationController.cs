using System;
using Ingosstrakh.AnimationController.AnimationBroadcaster;
using Ingosstrakh.Audio;
using UnityEngine;
using Zenject;

namespace Ingosstrakh.AnimationController
{
    public class AnimationController : MonoBehaviour, IStateMachineExitReceiver, IStateMachineEnterReceiver
    {
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private AudioClip moveInClip;
        [SerializeField] private AudioClip moveOutClip;
        [SerializeField] private AudioManager audioManager;
        private void OnEnable()
        {
            playerAnimator.SetTrigger(AnimationState.MoveIn.ToString());
        }

        void IStateMachineExitReceiver.OnAnimatorExitState(Animator animator, AnimatorStateInfo state)
        {
            if (state.IsTag(AnimationState.MoveOut.ToString()))
            {
                playerAnimator.gameObject.SetActive(false);
                audioManager.ResetAudio();
            }
        }
        void IStateMachineEnterReceiver.OnAnimatorEnterState(Animator animator, AnimatorStateInfo state)
        {
            if (state.IsTag(AnimationState.MoveIn.ToString()))
            {
                audioManager.PlayAudio(moveInClip);
            }
            else if (state.IsTag(AnimationState.MoveOut.ToString()))
            {
                audioManager.PlayAudio(moveOutClip);
            }
        }
    }
}