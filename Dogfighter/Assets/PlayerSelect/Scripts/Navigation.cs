using UnityEngine;
using Shared.Scripts.GameData;

namespace PlayerSelect.Scripts
{
    public class Navigation : MonoBehaviour
    {
        private void Update()
        {
            if (CurrentGame.ReadyToStartGame()) { Application.LoadLevel("TestScene"); }
            if (Input.GetKeyDown(KeyCode.Escape)) { Application.LoadLevel("Title"); }
        }
    }
}
