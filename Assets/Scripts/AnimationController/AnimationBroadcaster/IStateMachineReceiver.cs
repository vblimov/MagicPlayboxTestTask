using UnityEngine;

namespace Ingosstrakh.AnimationController.AnimationBroadcaster
{
    public interface IStateMachineEnterReceiver
    {
        void OnAnimatorEnterState(Animator animator, AnimatorStateInfo state);
    }

    public interface IStateMachineExitReceiver
    {
        void OnAnimatorExitState(Animator animator, AnimatorStateInfo state);
    }

    public interface IStateMachineUpdateReceiver
    {
        void OnAnimatorUpdateState(Animator animator, AnimatorStateInfo state);
    }
}