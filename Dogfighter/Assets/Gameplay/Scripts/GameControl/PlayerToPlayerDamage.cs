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

        private void Awake()
        {
            _instance = this;
            _healthControllers = new Dictionary<string,Health>();
        }

        public void RegisterPlayer(GameObject player)
        {
            _healthControllers.Add(player.tag, player.transform.FindChild("Status Manager").FindChild("Health").GetComponent<Health>());
        }

        private void SendDamageToPlayer(string targetId, float value, string sourceId)
        {
            if (_healthControllers.ContainsKey(targetId))
            {
                _healthControllers[targetId].TakeDamage(value, sourceId);
            }
        }
    }
}
