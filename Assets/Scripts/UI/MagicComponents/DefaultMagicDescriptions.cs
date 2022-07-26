using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ingosstrakh.UI.MagicComponents
{
    [CreateAssetMenu]
    public class DefaultMagicDescriptions : ScriptableObject
    {
        [Serializable]
        public struct DefaultMagicDescription
        {
            public MagicTag Tag;
            public MagicDescription Description;
        }

        [SerializeField] private List<DefaultMagicDescription> defaultMagicDescriptions = new();

        public MagicDescription GetDescription(MagicTag tag)
        {
            return defaultMagicDescriptions.FirstOrDefault(description => description.Tag == tag).Description;
        }
    }
}