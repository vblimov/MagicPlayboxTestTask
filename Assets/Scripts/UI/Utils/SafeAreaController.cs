using UnityEngine;

namespace Ingosstrakh.UI.Utils
{
    /// <summary>
    ///		This component adds a safe area to your UI.
    ///		This is mainly used to prevent UI elements from going through notches on mobile devices.
    ///		This component should be added to a GameObject that is a child of your Canvas root.
    /// </summary>
    public class SafeAreaController : MonoBehaviour
    {

        private readonly Vector2 verticalRange = new Vector2(0.0f, 1.0f);

        [System.NonSerialized]
        private RectTransform cachedRectTransform;

        [System.NonSerialized]
        private bool cachedRectTransformSet;

        private void Awake()
        {
            if (cachedRectTransformSet == false)
            {
                cachedRectTransform    = GetComponent<RectTransform>();
                cachedRectTransformSet = true;
            }

            var safeRect = UnityEngine.Screen.safeArea;
            var screenW  = UnityEngine.Screen.width;
            var screenH  = UnityEngine.Screen.height;
            var safeMin  = safeRect.min;
            var safeMax  = safeRect.max;


            safeMin.y = Mathf.Max(safeMin.y, verticalRange.x * screenH);
            safeMax.y = Mathf.Min(safeMax.y, verticalRange.y * screenH);


            cachedRectTransform.anchorMin = new Vector2(safeMin.x / screenW, safeMin.y / screenH);
            cachedRectTransform.anchorMax = new Vector2(safeMax.x / screenW, safeMax.y / screenH);

        }
    }
}