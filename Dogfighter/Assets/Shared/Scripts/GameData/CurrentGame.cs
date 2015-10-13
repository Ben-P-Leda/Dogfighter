using UnityEngine;
using System.Collections.Generic;

namespace Shared.Scripts.GameData
{
    public static class CurrentGame
    {
        private static Dictionary<string, PlayerData> _players = null;

        public static Dictionary<string, PlayerData> Players { get { return _players; } }

        public static void Reset()
        {
            if (_players == null) { CreatePlayerDataContainers(); }
            foreach (KeyValuePair<string, PlayerData> kvp in _players)
            {
                kvp.Value.Reset();
            }
        }

        private static void CreatePlayerDataContainers()
        {
            _players = new Dictionary<string, PlayerData>();
            for (int i=0; i<Maximum_Players; i++)
            {
                _players.Add(string.Format("Player {0}", i + 1), new PlayerData());
            }
        }

        public static void ActivatePlayer(string PlayerId)
        {
            _players[PlayerId].IsActive = true;
        }

        public static bool ReadyToStartGame()
        {
            int ReadyPlayerCount = 0;
            foreach (KeyValuePair<string, PlayerData> kvp in _players)
            {
                if (kvp.Value.Ready) { ReadyPlayerCount++; }
            }

            return ReadyPlayerCount == Maximum_Players;
        }

        public static void LogPlayerDeath(string deadPlayerId, string killingPlayerId)
        {
            if (_players.ContainsKey(deadPlayerId)) { _players[deadPlayerId].Deaths++; }
            if (_players.ContainsKey(killingPlayerId)) { _players[killingPlayerId].PlayerKills++; }

            Debug.Log(string.Format("{0} died, killed by {1}, {2} deaths so far...",
                deadPlayerId,
                string.IsNullOrEmpty(killingPlayerId) ? "crashing" : killingPlayerId,
                _players[deadPlayerId].Deaths));
        }

        public static string GetWinner()
        {
            string winningPlayerId = "";
            int winningScore = 0;

            foreach (KeyValuePair<string, PlayerData> kvp in _players)
            {
                int playerScore = kvp.Value.TotalScore;

                if (playerScore > winningScore)
                {
                    winningPlayerId = kvp.Key;
                    winningScore = playerScore;
                }
                else if (playerScore == winningScore)
                {
                    winningPlayerId = "";
                    break;
                }
            }

            return winningPlayerId;
        }

        public const int Maximum_Players = 2;
    }
}
