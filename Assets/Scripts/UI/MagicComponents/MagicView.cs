using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ingosstrakh.UI.MagicComponents
{
    public class MagicView : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private ProgressView progressView;
        [SerializeField] private MagicTag magicTag;
        private int starsPerClick = 0;
        public MagicTag Tag => magicTag;
        public event Action<MagicTag, int> PlayButtonPressed;

        private void Awake()
        {
            starsPerClick = ResourcesLoader.ResourceLoader.DefaultMagicDescriptions.StarsPerClick;
            playButton.onClick.AddListener(PlayMagic);
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveAllListeners();
        }

        private void PlayMagic()
        {
            Debug.Log("Play button pressed");
            PlayButtonPressed?.Invoke(magicTag, starsPerClick);
        }

        public void Init(int value, int maxValue)
        {
            progressView.Init(value, maxValue);
        }

        public void SetProgress(int value)
        {
            progressView.SetValue(value);
        }
    }
}