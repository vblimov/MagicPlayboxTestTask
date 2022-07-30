using Ingosstrakh.Audio;
using UnityEngine;
using Zenject;

namespace Ingosstrakh.Installers
{
    
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private AudioManager audioManager;
        public override void InstallBindings()
        {
            Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle().NonLazy();
        }
    }
}