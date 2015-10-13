using UnityEngine;
using Shared.Scripts.GameData;

namespace Gameplay.Scripts.GameControl.PostGame
{
    public class WinnerText : EndRoundText
    {
        private void Awake()
        {
            string winner = CurrentGame.GetWinner();

            Text = string.IsNullOrEmpty(winner) ? "Draw!" : string.Format("{0} Wins!", winner);
        }
    }
}
