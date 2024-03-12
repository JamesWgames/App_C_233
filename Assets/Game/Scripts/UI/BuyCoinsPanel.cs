using Prototype.AudioCore;
using Slots.Game.Values;
using UI.Panels;

namespace UI
{
    public class BuyCoinsPanel : Panel
    {
        public void AddCoins(int coins)
        {
            AudioController.PlaySound("coins");
            
            Wallet.AddMoney(coins);
        }
    }
}