using System;
using UnityEngine;

namespace Ingosstrakh.AnimationController
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private void OnEnable()
        {
            animator.Play("Step 1");
        }

        private void PlayAnimationRange(AnimationConfig.AnimationDescription animationDescription)
        {
        }
    }
}