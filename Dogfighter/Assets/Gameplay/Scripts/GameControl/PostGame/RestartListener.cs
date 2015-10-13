using UnityEngine;
using System.Collections.Generic;
using Shared.Scripts.GameData;

namespace Gameplay.Scripts.GameControl.PostGame
{
    public class RestartListener : MonoBehaviour
    {
        private bool _readyForExit;

        private void Awake()
        {
            _readyForExit = false;
        }

        private void FixedUpdate()
        {
            if ((!_readyForExit) && (!AnyButtonHeld())) { _readyForExit = true; }
            else if (AnyButtonHeld()) { Application.LoadLevel("Title"); }
        }

        private bool AnyButtonHeld()
        {
            bool buttonHeld = false;

            foreach(KeyValuePair<string, PlayerData> kvp in CurrentGame.Players)
            {
                if ((kvp.Value.IsActive) && (Input.GetAxis(kvp.Key + " Fire1") > 0.0f))
                {
                    buttonHeld = true;
                }
            }

            return buttonHeld;
        }
    }
}
