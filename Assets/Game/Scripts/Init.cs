using Extension;
using Prototype.SceneLoaderCore.Helpers;
using Slots.Game.Values;
using UnityEngine;

public class Init : MonoBehaviour
{
    [Header("Main scene"), Scene]
    [SerializeField] private string _mainScene;
    
    private void Awake()
    {
        Input.multiTouchEnabled = false;
        
        QualitySettings.vSyncCount = 0;
        
        Application.targetFrameRate = 60;
        
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (!PlayerPrefs.HasKey("FirstMoneyBonus"))
        {
            Wallet.AddMoney(500);

            PlayerPrefs.SetInt("FirstMoneyBonus", 500);
        }
    }

    private void Start()
    {
        SceneLoader.Instance.SwitchToScene(_mainScene);
    }
}
