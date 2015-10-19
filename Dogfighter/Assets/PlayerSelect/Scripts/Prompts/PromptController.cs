using UnityEngine;
using Shared.Scripts.Gui;

namespace PlayerSelect.Scripts.Prompts
{
    public class PromptController : GuiManager
    {
        private PromptText _text;
        private JoiningTimer _timer;

        protected override void Awake()
        {
            base.Awake();

            _text = GetComponent<PromptText>();
            _timer = GetComponent<JoiningTimer>();
        }

        private void Start()
        {
            _text.SetUpDisplay();

            if (_timer != null) { _timer.SetUpDisplay(); }
        }

        protected override Camera GetCamera()
        {
            return transform.parent.FindChild("Camera").GetComponent<Camera>();
        }
    }
}
