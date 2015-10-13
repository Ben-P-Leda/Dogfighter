using UnityEngine;
using Shared.Scripts.Gui;

namespace Title.Scripts.Chrome
{
    public class ChromeText : GuiText
    {
        public void SetUpDisplay()
        {
            GuiManager manager = transform.parent.GetComponent<GuiManager>();
            base.SetUpDisplay(manager.ViewportScreenArea, manager.Scaling);
        }
    }
}
