using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using JsonSerializer = Ingosstrakh.ResourcesLoader.JsonSerializer;

namespace Ingosstrakh.UI.MagicComponents
{
    public class MagicController : MonoBehaviour
    {
        [SerializeField] private List<MagicView> magicViews;
        private MagicData magicData;
        private const float ScoreIncreaseCooldown = 1.25f;
        private bool isCooldowned = false;
        public event Action<int> OnMagicScoreIncreased;
        public event Action<int> OnMagicScoreLoaded;

        private void Start()
        {
            magicViews.ForEach(mag => mag.PlayButtonPressed += UpdateCounter);
            LoadMagicData();
            ShowStarsCount();
        }

        private void LoadMagicData()
        {
            JsonSerializer.LoadLocal(JsonSerializer.DefaultPath + nameof(MagicData), out magicData);
            magicData ??= new MagicData(true);
            OnMagicScoreLoaded?.Invoke(magicData.GetScore());
        }

        private void OnDestroy()
        {
            JsonSerializer.SaveLocal(JsonSerializer.DefaultPath + nameof(MagicData), magicData);
            magicViews.ForEach(mag => mag.PlayButtonPressed -= UpdateCounter);
        }
        private void ShowStarsCount()
        {
            foreach (var view in magicViews)
            {
                var desc = magicData.GetMagicDescription(view.Tag);
                view.Init(desc.Value, desc.MaxValue);
            }
        }
        private void ShowStarsCount(MagicTag magicTag)
        {
           magicViews.Find(m => m.Tag == magicTag).SetProgress(magicData.GetMagicDescription(magicTag).Value);
        }

        private async void UpdateCounter(MagicTag magicTag, int delta)
        {
            if (isCooldowned)
            {
                return;
            }
            var magicDescription = magicData.GetMagicDescription(magicTag);
            if (magicDescription.Value + delta <= magicDescription.MaxValue)
            {
                magicData.ChangeMagicDescriptionValue(magicTag, delta);
                ShowStarsCount(magicTag);
                OnMagicScoreIncreased?.Invoke(delta);
            }

            isCooldowned = true;
            await Task.Delay((int)(ScoreIncreaseCooldown * 1000));
            isCooldowned = false;
            
        }
    }
}