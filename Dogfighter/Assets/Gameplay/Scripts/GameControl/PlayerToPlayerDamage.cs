using UnityEngine;
using System.Collections.Generic;
using Gameplay.Scripts.Status;

namespace Gameplay.Scripts.GameControl
{
    public class PlayerToPlayerDamage : MonoBehaviour
    {
        private static PlayerToPlayerDamage _instance;
        public static void InflictDamage(string targetId, float value, string sourceId) { _instance.SendDamageToPlayer(targetId, value, sourceId); }

        private Dictionary<string, Health> _healthControllers;

        public List<GameObject> Players;

        private void Awake()
        {
            _instance = this;

            _healthControllers = new Dictionary<string,Health>();
            for (int i=0; i<Players.Count; i++)
            {
                _healthControllers.Add(Players[i].tag, Players[i].transform.FindChild("Status Manager").FindChild("Health").GetComponent<Health>());
            }
        }

        private void SendDamageToPlayer(string targetId, float value, string sourceId)
        {
            _healthControllers[targetId].TakeDamage(value, sourceId);
        }
    }
}
