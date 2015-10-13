using UnityEngine;
using Shared.Scripts.Gui;

namespace Gameplay.Scripts.GameControl.PostGame
{
    public class EndRoundDisplayManager : GuiManager
    {
        private EndRoundText[] _texts;

        protected override void Awake()
        {
            base.Awake();

            Transform textContainer = transform.FindChild("Texts");

            _texts = new EndRoundText[textContainer.childCount];
            for (int i = 0; i < textContainer.childCount; i++)
            {
                _texts[i] = textContainer.GetChild(i).GetComponent<EndRoundText>();
            }
        }

        private void Start()
        {
            for (int i = 0; i < _texts.Length; i++)
            {
                _texts[i].SetUpDisplay();
            }
        }

        protected override Camera GetCamera()
        {
            return transform.FindChild("Camera").GetComponent<Camera>();
        }
    }
}
