using UnityEngine;
using Shared.Scripts.Gui;

namespace Gameplay.Scripts.GameControl.PostGame
{
    public class EndRoundText : GuiText
    {
        public virtual void SetUpDisplay()
        {
            GuiManager manager = transform.parent.parent.GetComponent<GuiManager>();
            base.SetUpDisplay(manager.ViewportScreenArea, manager.Scaling);
        }
    }
}
