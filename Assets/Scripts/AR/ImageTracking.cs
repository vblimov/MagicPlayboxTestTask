using System;
using Ingosstrakh.Signals;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Zenject;

namespace Ingosstrakh.AR
{
    public class ImageTracking : MonoBehaviour
    {
        [SerializeField] private GameObject placeablePrefab;
        private GameObject spawnedPrefab;
        [Inject]
        private ARTrackedImageManager arTrackedImageManager;

        public void ImagesChanged(ARTrackedImageSignal signal)
        {
            var eventArgs = signal.TrackedImagesChangedEventArgs;
            foreach (var trackedImage in eventArgs.added)
            {
                UpdateImage(trackedImage);
            }
            foreach (var trackedImage in eventArgs.updated)
            {
                UpdateImage(trackedImage);
            }
            foreach (var trackedImage in eventArgs.removed)
            {
                Destroy(spawnedPrefab);
                spawnedPrefab = null;
            }
        }

        private void UpdateImage(Component trackedImage)
        {
            
            var position = trackedImage.transform.position;
            if (spawnedPrefab == null)
            {
                spawnedPrefab = Instantiate(placeablePrefab, position, Quaternion.identity);
            }
            else
            {
                spawnedPrefab.transform.position = position;
            }
        }
    }
}