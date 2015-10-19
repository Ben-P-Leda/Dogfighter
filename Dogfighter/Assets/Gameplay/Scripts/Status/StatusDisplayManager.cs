using UnityEngine;
using Shared.Scripts.Gui;
using Gameplay.Scripts.Status.Messages;

namespace Gameplay.Scripts.Status
{
    public class StatusDisplayManager : GuiManager
    {
        private Health _healthController;

        private MessageText _startMessagePopup;
        private RestartTimer _restartTimer;

        public string PlayerId { get; private set; }
        public bool ReadyToRespawn { get { return _restartTimer.ReadyToRespawn; } }

        protected override void Awake()
        {
            base.Awake();

            PlayerId = transform.parent.tag;

            _healthController = transform.FindChild("Health").GetComponent<Health>();

            _startMessagePopup = transform.FindChild("Messages").FindChild("Life Start").GetComponent<MessageText>();
            _restartTimer = transform.FindChild("Messages").FindChild("Restart Timer").GetComponent<RestartTimer>();
        }

        private void Start()
        {
            _startMessagePopup.SetUpDisplay();
            _restartTimer.SetUpDisplay();

            transform.FindChild("Radar").GetComponent<Radar>().SetUpDisplay();
        }

        protected override Camera GetCamera()
        {
            return transform.parent.FindChild("Camera").GetComponent<Camera>();
        }

        public void SetForNewLife()
        {
            _healthController.SetForNewLife();
            _startMessagePopup.Activate();
        }

        public void SetForDeath()
        {
            _restartTimer.Activate();
        }
    }
}
