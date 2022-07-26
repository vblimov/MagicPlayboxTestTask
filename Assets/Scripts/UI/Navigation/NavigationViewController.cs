using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ingosstrakh.UI.Navigation
{
    public class NavigationViewController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup leftArrow;
        [SerializeField] private CanvasGroup rightArrow;
        [SerializeField] private Scrollbar scrollbar;
        [Tooltip("Offset of scrollbar, when navigation arrows will finally showed/hided")]
        [SerializeField] private float scrollOffset = 0.2f;
        private float AlphaMultiplier => 1 / scrollOffset;

        private void Awake()
        {
            scrollbar.onValueChanged.AddListener(UpdateArrowsVisible);
            UpdateArrowsVisible(scrollbar.value);
        }

        private void OnDestroy()
        {
            scrollbar.onValueChanged.RemoveListener(UpdateArrowsVisible);
        }

        private void UpdateArrowsVisible(float value)
        {
            if (value > scrollOffset && value < 1 - scrollOffset) { return; }
            leftArrow.alpha = value * AlphaMultiplier;
            rightArrow.alpha = (1 - value) * AlphaMultiplier;
        }
        
    }
}