using System.Threading.Tasks;
using Ingosstrakh.Signals;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

namespace Ingosstrakh.AR
{
    public class ARTrackedImageProvider : IInitializable
    {
        private readonly SignalBus signalBus;
        private readonly ARTrackedImageManager arTrackedImageManager;

        public ARTrackedImageProvider(SignalBus signalBus, ARTrackedImageManager arTrackedImageManager)
        {
            this.signalBus = signalBus;
            this.arTrackedImageManager = arTrackedImageManager;
            arTrackedImageManager.trackedImagesChanged += FireEvent;
        }

        ~ARTrackedImageProvider()
        {
            arTrackedImageManager.trackedImagesChanged -= FireEvent;
        }

        public void Initialize()
        {
            // await Task.Delay(5000);
            // signalBus.Fire(new ARTrackedImageSignal() {TrackedImagesChangedEventArgs = default});
        }

        private void FireEvent(ARTrackedImagesChangedEventArgs args)
        {
            signalBus.Fire(new ARTrackedImageSignal() {TrackedImagesChangedEventArgs = args});
        }
    }
}