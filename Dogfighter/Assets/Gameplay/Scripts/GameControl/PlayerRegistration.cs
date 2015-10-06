using UnityEngine;
using System.Collections.Generic;

namespace Gameplay.Scripts.GameControl
{
    public class PlayerRegistration : MonoBehaviour
    {
        public List<GameObject> Players;

        private void Awake()
        {
            PlayerToPlayerDamage playerDamageHandler = GetComponent<PlayerToPlayerDamage>();
            PlayerPositionTracker playerTracker = GetComponent<PlayerPositionTracker>();
            PlayerDeathHandler playerDeathHandler = GetComponent<PlayerDeathHandler>();

            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].activeInHierarchy)
                {
                    playerDamageHandler.RegisterPlayer(Players[i]);
                    playerTracker.RegisterPlayer(Players[i]);
                    playerDeathHandler.RegisterPlayer(Players[i]);
                }
            }

            playerTracker.WireUpRadars();
        }
    }
}
