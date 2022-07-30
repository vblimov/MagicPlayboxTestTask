using Ingosstrakh.Audio;
using Ingosstrakh.Signals;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace Ingosstrakh.Installers
{
    public class ARInstaller : MonoInstaller
    {
        [SerializeField] private ARTrackedImageManager arTrackedImageManager;
        public override void InstallBindings()
        {
            Container.Bind<ARTrackedImageManager>().FromInstance(arTrackedImageManager).AsCached().NonLazy();
        }
    }
}