using UnityEngine;

namespace Ingosstrakh.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        public void PlayAudio(AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void ResetAudio()
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}