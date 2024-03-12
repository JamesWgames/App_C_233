using Extension;
using Prototype.AudioCore;
using Prototype.SceneLoaderCore.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private string _sceneName = "Scene";

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioController.Release();
                
                SceneLoader.Instance.SwitchToScene(_sceneName);
            });
        }
    }
}