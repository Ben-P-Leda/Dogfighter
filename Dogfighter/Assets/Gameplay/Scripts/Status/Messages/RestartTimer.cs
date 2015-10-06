using UnityEngine;
using Shared.Scripts;
using Gameplay.Scripts.GameControl;

namespace Gameplay.Scripts.Status.Messages
{
    public class RestartTimer : MessageText
    {
        private int _displayTimeRemaining;

        public int TimeToRestart { private get; set; }
        public bool ReadyToRespawn { get; private set; }

        protected override void Awake()
        {
            TimeToRestart = 2;
            _displayTimeRemaining = 0;

            base.Awake();
        }

        public override void Activate()
        {
            ReadyToRespawn = false;

            base.Activate();

            if (_displayTimeRemaining == 0) 
            {
                _displayTimeRemaining = TimeToRestart;
            }
            else
            {
                _displayTimeRemaining--;
            }

            Text = string.Format("New plane in {0}", _displayTimeRemaining + 1);
        }

        protected override void Deactivate()
        {
            if (_displayTimeRemaining > 0)
            {
                Activate();
            }
            else
            {
                ReadyToRespawn = true;
                gameObject.SetActive(false);
            }
        }
    }
}
