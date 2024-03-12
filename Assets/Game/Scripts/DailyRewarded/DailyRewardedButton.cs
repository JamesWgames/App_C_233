using System;
using UnityEngine;
using UnityEngine.UI;

namespace DailyRewarded
{
    [RequireComponent(typeof(Button))]
    public class DailyRewardedButton : MonoBehaviour
    {
        [SerializeField] private DailyRewardedPanel _panel = null;
        [SerializeField] private Button _button = null;
        [SerializeField] private Text _timer = null;

        private void Awake()
        {
            SubscribeToButton();
        }

        private void Update()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            if (DailyRewardedPanel.IsReachedPrizeToday)
            {
                _button.interactable = false;

                TimeSpan span = DateTime.Now.AddDays(1).Date - DateTime.Now;
                
                _timer.text = $"{span.Hours:00}:{span.Minutes:00}:{span.Seconds:00}";
            }
            else
            {
                _button.interactable = true;

                _timer.text = "";
            }
        }

        private void SubscribeToButton()
        {
            _button.onClick.AddListener(ShowDailyPanel);
        }

        private void ShowDailyPanel()
        {
            _panel.Show();
        }
    }
}
