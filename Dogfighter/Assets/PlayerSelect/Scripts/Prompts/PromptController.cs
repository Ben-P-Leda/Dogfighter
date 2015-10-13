using UnityEngine;
using Shared.Scripts.Gui;

namespace PlayerSelect.Scripts.Prompts
{
    public class PromptController : GuiManager
    {
        private PromptText _text;

        protected override void Awake()
        {
            base.Awake();

            _text = GetComponent<PromptText>();
        }

        private void Start()
        {
            _text.SetUpDisplay();
        }

        protected override Camera GetCamera()
        {
            return transform.parent.FindChild("Camera").GetComponent<Camera>();
        }
    }
}
