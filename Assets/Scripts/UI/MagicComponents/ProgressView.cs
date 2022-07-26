using TMPro;
using UnityEngine;

namespace Ingosstrakh.UI.MagicComponents
{
    public class ProgressView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentValueText;
        [SerializeField] private TextMeshProUGUI maxValueText;

        public int MaxValue { get; private set; } = 0;
        public int Value { get; private set; } = 0;

        public void Init(int value, int maxValue)
        {
            Value = value;
            MaxValue = maxValue;
            SetTexts();
        }
        public void SetValue(int value)
        {
            Value = value;
            SetTexts();
        }

        private void SetTexts()
        {
            currentValueText.text = Value.ToString();
            maxValueText.text = MaxValue.ToString();
        }
    }
}