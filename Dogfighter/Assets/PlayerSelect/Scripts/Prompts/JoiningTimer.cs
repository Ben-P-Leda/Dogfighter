using UnityEngine;
using Shared.Scripts;
using Shared.Scripts.GameData;
using Shared.Scripts.Gui;

namespace PlayerSelect.Scripts.Prompts
{
    public class JoiningTimer : GuiText
    {
        private string _playerId;
        private float _timeRemaining;

        private void Awake()
        {
            _playerId = transform.parent.tag;
            _timeRemaining = Maximum_Joining_Time;

            Text = "";
        }

        public void SetUpDisplay()
        {
            GuiManager manager = transform.GetComponent<GuiManager>();

            Debug.Log(transform.parent.tag + ": " + manager.ViewportScreenArea.ToString());

            base.SetUpDisplay(manager.ViewportScreenArea, manager.Scaling);
        }

        private void FixedUpdate()
        {
            if (CurrentGame.ReadyPlayerCount() >= Minimum_Player_Count)
            {
                UpdateTimer();
                Text = string.Format("Round starts in {0}", Utility.TimeText(_timeRemaining));
            }
        }

        private void UpdateTimer()
        {
            _timeRemaining = Mathf.Max(0.0f, _timeRemaining - Time.deltaTime);

            if (_timeRemaining <= 0.0f)
            {
                CurrentGame.Players[_playerId].Ready = true;
            }
        }

        private const int Minimum_Player_Count = 2;
        private const float Maximum_Joining_Time = 7.0f;
    }
}
