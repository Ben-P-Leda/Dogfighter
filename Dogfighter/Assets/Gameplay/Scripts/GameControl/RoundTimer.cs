using UnityEngine;

namespace Gameplay.Scripts.GameControl
{
    public class RoundTimer : MonoBehaviour
    {
        private static RoundTimer _instance;
        public static float TimeRemaining { get { return _instance._timeRemaining; } }
        public static bool RoundOver { get { return _instance._timeRemaining <= 0.0f; } }

        private float _timeRemaining;

        public float RoundDurationInSeconds;

        private void Awake()
        {
            _instance = this;
            _timeRemaining = RoundDurationInSeconds;
        }

        private void Update()
        {
            _timeRemaining = Mathf.Max(0.0f, _timeRemaining - Time.deltaTime);
        }
    }
}
