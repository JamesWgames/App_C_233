using Prototype.AudioCore;
using Slots.Game.Values;
using Tools.Core.UnityAdsService.Scripts;
using UI.Panels;
using UnityEngine;

namespace Game
{
    public class NoMoneyPanel : Panel
    {
        [SerializeField] private UnityAdsButton _adsButton = null;

        private void Awake()
        {
            _adsButton.OnCanGetReward += GivePrize;
        }

        private void GivePrize()
        {
            AudioController.PlaySound("coins");

            Wallet.AddMoney(100);
            
            Hide();
        }
    }
}
