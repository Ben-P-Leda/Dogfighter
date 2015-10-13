using UnityEngine;
using System.Collections.Generic;
using Shared.Scripts.GameData;
using Gameplay.Scripts.Player;

namespace Gameplay.Scripts.GameControl
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        private static PlayerDeathHandler _instance;
        public static void TriggerDeath(string deadPlayerId, string killingPlayerId) { _instance.HandleDeath(deadPlayerId, killingPlayerId); }

        private Dictionary<string, LifeCycle> _lifeCycleControllers;

        private void Awake()
        {
            _instance = this;
            _lifeCycleControllers = new Dictionary<string, LifeCycle>();
        }

        public void RegisterPlayer(GameObject player)
        {
            _lifeCycleControllers.Add(player.tag, player.GetComponent<LifeCycle>());
        }

        private void HandleDeath(string deadPlayerId, string killingPlayerId)
        {
            _lifeCycleControllers[deadPlayerId].LaunchDeathSequence();

            CurrentGame.LogPlayerDeath(deadPlayerId, killingPlayerId);
        }
    }
}
