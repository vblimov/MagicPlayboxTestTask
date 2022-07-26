using System;
using System.Collections.Generic;
using System.Linq;

namespace Ingosstrakh.UI.MagicComponents
{
    [Serializable]
    public class MagicData
    {
        public Dictionary<MagicTag, MagicDescription> MagicStoredData = new();

        public MagicData(bool useDefaultDescriptions)
        {
            var defaultMagicDescriptions = ResourcesLoader.ResourceLoader.DefaultMagicDescriptions;
            foreach (var tag in Enum.GetValues(typeof(MagicTag)).Cast<MagicTag>())
            {
                var desc = defaultMagicDescriptions.GetDescription(tag);
                MagicStoredData.Add(tag, new MagicDescription(desc.Value, desc.MaxValue));
            }
        }
        public MagicData() { }

        public MagicDescription GetMagicDescription(MagicTag tag)
        {
            if (!MagicStoredData.ContainsKey(tag))
            {
                var desc = ResourcesLoader.ResourceLoader.DefaultMagicDescriptions.GetDescription(tag);
                MagicStoredData.Add(tag, new MagicDescription(desc.Value, desc.MaxValue));
            }
            return MagicStoredData[tag];
        }

        public int GetScore()
        {
            return MagicStoredData.Select(kv => kv.Value).Sum(desc => desc.Value);
        }
        public void ChangeMagicDescriptionValue(MagicTag tag, int delta)
        {
            GetMagicDescription(tag).Value += delta;
        }
    }
}