using UnityEngine;
using System.Collections.Generic;
using Shared.Scripts.GameData;

namespace Gameplay.Scripts.GameControl
{
    public class PlayerRegistration : MonoBehaviour
    {
        private bool _debugging;

        public List<GameObject> Players;

        private void Awake()
        {
            _debugging = false;
            if (CurrentGame.Players == null) 
            { 
                CurrentGame.Reset();
                _debugging = true;
            }
        }

        private void Start()
        {
            PlayerToPlayerDamage playerDamageHandler = GetComponent<PlayerToPlayerDamage>();
            PlayerPositionTracker playerTracker = GetComponent<PlayerPositionTracker>();
            PlayerDeathHandler playerDeathHandler = GetComponent<PlayerDeathHandler>();

            EndRoundHandler endRoundHandler = GetComponent<EndRoundHandler>();

            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].activeInHierarchy)
                {
                    playerDamageHandler.RegisterPlayer(Players[i]);
                    playerTracker.RegisterPlayer(Players[i]);
                    playerDeathHandler.RegisterPlayer(Players[i]);
                    endRoundHandler.RegisterPlayer(Players[i]);

                    if (_debugging)
                    {
                        CurrentGame.Players[Players[i].tag].IsActive = true;
                    }
                }
            }

            playerTracker.WireUpRadars();
        }
    }
}
