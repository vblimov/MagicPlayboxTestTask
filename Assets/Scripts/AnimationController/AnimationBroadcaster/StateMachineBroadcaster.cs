using UnityEngine;

namespace Ingosstrakh.AnimationController.AnimationBroadcaster
{
    public class StateMachineBroadcaster : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var enterReceiver in animator.GetComponentsInChildren<IStateMachineEnterReceiver>())
            {
                if (enterReceiver != null)
                {
                    enterReceiver.OnAnimatorEnterState(animator, stateInfo);
                }
            }
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var exitReceiver in animator.GetComponentsInChildren<IStateMachineExitReceiver>())
            {
                if (exitReceiver != null)
                {
                    exitReceiver.OnAnimatorExitState(animator, stateInfo);
                }
            }
            base.OnStateExit(animator, stateInfo, layerIndex);
        }
    }
}
