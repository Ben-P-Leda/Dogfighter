﻿using UnityEngine;
using Shared.Scripts.GameData;
using Shared.Scripts.Gui;

namespace PlayerSelect.Scripts.Prompts
{
    public class PromptText : GuiText
    {
        private string _playerId;

        private void Awake()
        {
            _playerId = transform.parent.tag;

            Text = CurrentGame.Players[_playerId].IsActive
                ? "Select Player"
                : "Press Button to Start";
        }

        public void SetUpDisplay()
        {
            GuiManager manager = transform.GetComponent<GuiManager>();
            base.SetUpDisplay(manager.ViewportScreenArea, manager.Scaling);
        }

        private void Update()
        {
            Debug.Log(_playerId + ":" + CurrentGame.Players[_playerId].IsActive.ToString());

            if (CurrentGame.Players[_playerId].IsActive)
            {
                Text = CurrentGame.Players[_playerId].Ready
                    ? "Waiting for Opponents..."
                    : "Select Player";
            }
        }
    }
}
