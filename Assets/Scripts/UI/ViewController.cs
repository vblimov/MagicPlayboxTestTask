using System;
using System.Collections.Generic;
using Ingosstrakh.UI.MagicComponents;
using UnityEngine;

namespace Ingosstrakh.UI
{
    public class ViewController : MonoBehaviour
    {
        [SerializeField] private ScoreView.ScoreView score;
        [SerializeField] private MagicController magicController;
        private int starsCounter = 0;

        private void Awake()
        {
            magicController.OnMagicScoreIncreased += UpdateCounter;
            magicController.OnMagicScoreLoaded += SetCounter;
        }

        private void OnDestroy()
        {
            magicController.OnMagicScoreIncreased -= UpdateCounter;
            magicController.OnMagicScoreLoaded -= SetCounter;
        }

        private void UpdateCounter(int delta)
        {
            ShowStarsCount(starsCounter, delta);
            starsCounter += delta;
        }

        private void SetCounter(int value)
        {
            starsCounter = value;
            ShowStarsCountSilent();
        }

        private void ShowStarsCountSilent()
        {
            score.SetScoreSilent(starsCounter);
        }
        private void ShowStarsCount(int current, int delta)
        {
            score.SetScore(current, delta);
        }
    }
}