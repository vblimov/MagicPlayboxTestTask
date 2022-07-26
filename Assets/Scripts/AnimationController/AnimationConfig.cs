using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ingosstrakh.AnimationController
{
    public class AnimationConfig : ScriptableObject
    {
        [Serializable]
        public struct AnimationDescription
        {
            public AnimationClip clip;
            public AnimationState state;
        }

        [SerializeField] private List<AnimationDescription> animationDescriptions = new();

        public AnimationDescription GetDescription(AnimationState state)
        {
            return animationDescriptions.FirstOrDefault(a => a.state == state);
        }
        
    }
}