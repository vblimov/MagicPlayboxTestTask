using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ingosstrakh.UI.Screens
{
    public class MainMenuScreen : MonoBehaviour
    {
        [SerializeField] private Button uiButton;
        [SerializeField] private Button arButton;

        private void Awake()
        {
            uiButton.onClick.AddListener(OpenUIScene);
            arButton.onClick.AddListener(OpenARScene);
        }

        private void OnDestroy()
        {
            uiButton.onClick.RemoveListener(OpenUIScene);
            arButton.onClick.RemoveListener(OpenARScene);
        }

        private async void OpenARScene()
        {
            var arSceneLoading = SceneManager.LoadSceneAsync("1-AR");
            var animSceneLoading = SceneManager.LoadSceneAsync("1-Animation", LoadSceneMode.Additive);
            animSceneLoading.allowSceneActivation = false;
            arSceneLoading.allowSceneActivation = false;
            while (animSceneLoading.progress < 0.9f && arSceneLoading.progress < 0.9f)
            {
                await Task.Delay(100);
            }
            animSceneLoading.allowSceneActivation = true;
            arSceneLoading.allowSceneActivation = true;
        }

        private void OpenUIScene()
        {
            SceneManager.LoadSceneAsync("2-UI");
        }
    }
}