using Ingosstrakh.AR;
using Ingosstrakh.Signals;
using UnityEngine;
using Zenject;

namespace Ingosstrakh.Installers
{
    public class AnimationInstaller : MonoInstaller
    {
        [SerializeField] private ImageTracking imageTracking;
        public override void InstallBindings()
        {
            Container.Bind<ImageTracking>().FromInstance(imageTracking).AsSingle();

            Container.BindSignal<ARTrackedImageSignal>()
                .ToMethod<ImageTracking>(x => x.ImagesChanged).FromResolve();
        }
    }
}