using Prototype.AudioCore;
using UnityEngine;

namespace Game
{
    public class SlotsSounds : MonoBehaviour
    {
        public void PlayClick()
        {
            AudioController.PlaySound("click");
        }
        
        public void PlaySpin()
        {
            AudioController.PlaySound("slots");
        }
        
        public void PlayWin()
        {
            AudioController.PlaySound("win");
        }
        
        public void PlayLose()
        {
            AudioController.PlaySound("lose");
        }
        
        public void PlayNoMoney()
        {
            AudioController.PlaySound("no_money");
        }
    }
}
