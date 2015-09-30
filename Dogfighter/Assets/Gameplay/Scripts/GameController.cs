using UnityEngine;
using System.Collections.Generic;

namespace Gameplay.Scripts
{
    public class GameController : MonoBehaviour
    {
        private static Dictionary<string, float> _health;

        private void Awake()
        {
            _health = new Dictionary<string, float>();
        }

        public static void RegisterPlayer(string playerId)
        {
            _health.Add(playerId, 100.0f);
        }

        public static float Health(string playerId)
        {
            return _health[playerId];
        }
    }
}
