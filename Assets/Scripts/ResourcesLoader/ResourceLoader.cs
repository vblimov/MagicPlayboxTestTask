using Ingosstrakh.UI.MagicComponents;
using UnityEngine;

namespace Ingosstrakh.ResourcesLoader
{
    public static class ResourceLoader
    {
        private static DefaultMagicDescriptions cachedDefaultMagicDescriptions;
        public static DefaultMagicDescriptions DefaultMagicDescriptions 
        {
            get 
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    return AssetDatabaseUtils.GetAssetOfType<DefaultMagicDescriptions>();
                }
#endif
                if (!cachedDefaultMagicDescriptions)
                {
                    Debug.Log("DefaultMagicDescriptions will be loaded");
                    cachedDefaultMagicDescriptions = Resources.Load<DefaultMagicDescriptions>("DefaultMagicDescriptions");
                }
                return cachedDefaultMagicDescriptions;
            }
        }
    }
}