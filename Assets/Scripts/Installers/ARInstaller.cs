using Ingosstrakh.AR;
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
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ARTrackedImageSignal>();
            Container.Bind<ARTrackedImageManager>().FromInstance(arTrackedImageManager).AsCached().NonLazy();
            Container.BindInterfacesTo<ARTrackedImageProvider>().AsSingle();
        }
    }
}