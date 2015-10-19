using UnityEngine;
using Shared.Scripts.GameData;

namespace PlayerSelect.Scripts
{
    public class Navigation : MonoBehaviour
    {
        private void Update()
        {
            if (ReadyToStartGame()) { Application.LoadLevel("TestScene"); }
            if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel("Title"); }
        }

        private bool ReadyToStartGame()
        {
            bool readyToStart = false;

            if (CurrentGame.ReadyPlayerCount() == CurrentGame.Maximum_Players) { readyToStart = true; }

            return readyToStart;
        }
    }
}
