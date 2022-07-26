using System;

namespace Ingosstrakh.UI.MagicComponents
{
    [Serializable]
    public class MagicDescription
    {
        public int Value;
        public int MaxValue;

        public MagicDescription(int value, int maxValue)
        {
            Value = value;
            MaxValue = maxValue;
        }
    }
}