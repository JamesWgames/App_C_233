using Prototype.SceneLoaderCore.Helpers;
using UI.Panels;

namespace UI
{
    public class ExitPanel : Panel
    {
        public void Confirm()
        {
            SceneLoader.Instance.SwitchToScene("Menu");
        }
        
        public void Cancel()
        {
            Hide();
        }
    }
}