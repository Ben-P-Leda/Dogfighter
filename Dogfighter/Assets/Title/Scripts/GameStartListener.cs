using UnityEngine;
using Shared.Scripts.GameData;

namespace Title.Scripts
{
    public class GameStartListener : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Resetting game data");
            CurrentGame.Reset();
        }

        private void Update()
        {
            bool gameStarted = false;
            for (int i=0; i<CurrentGame.Maximum_Players; i++)
            {

                if (Input.GetAxis(string.Format(Player_Id_Format, i + 1) + " Fire1") > 0.0f)
                {
                    CurrentGame.ActivatePlayer(string.Format(Player_Id_Format, i + 1));
                    gameStarted = true;
                }
            }

            if (gameStarted)
            {
                Application.LoadLevel("PlayerSelectScene");
            }

            if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        }

        private const string Player_Id_Format = "Player {0}";
    }
}