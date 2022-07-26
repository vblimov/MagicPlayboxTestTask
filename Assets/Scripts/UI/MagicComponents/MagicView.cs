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

        public MagicTag Tag => magicTag;
        public event Action<MagicTag, int> PlayButtonPressed;

        private void Awake()
        {
            playButton.onClick.AddListener(PlayMagic);
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveAllListeners();
        }

        private void PlayMagic()
        {
            Debug.Log("Play button pressed");
            PlayButtonPressed?.Invoke(magicTag, 5);
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