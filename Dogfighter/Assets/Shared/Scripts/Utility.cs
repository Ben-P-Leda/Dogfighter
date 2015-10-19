using UnityEngine;

namespace Shared.Scripts
{
    public static class Utility
    {
        public static string TimeText(float timerValue)
        {
            return string.Format("{0}:{1}{2}",
                Mathf.FloorToInt(Mathf.Max(timerValue / 60.0f, 0.0f)),
                Mathf.Max(timerValue % 60.0f) < 10.0f ? "0" : "",
                Mathf.FloorToInt(Mathf.Max(timerValue % 60.0f)));
        }
    }
}
