using UnityEngine;
using Shared.Scripts;
using Gameplay.Scripts.GameControl;

namespace Gameplay.Scripts.Status
{
    public class Timer : MonoBehaviour
    {
        private Rect _displayArea;
        private GUIStyle _guiStyle;
        private string _displayText;

        public Font Font;

        private void Start()
        {
            SetUpDisplay();
        }

        private void SetUpDisplay()
        {
            StatusDisplayManager manager = transform.parent.GetComponent<StatusDisplayManager>();

            _displayArea = manager.ScaleToDisplay(200.0f, 20.0f, TextAnchor.UpperCenter, 0.0f, 30.0f);

            _guiStyle = new GUIStyle();
            _guiStyle.font = Font;
            _guiStyle.fontSize = (int)(26 * manager.Scaling);
            _guiStyle.wordWrap = true;
            _guiStyle.alignment = TextAnchor.UpperCenter;
            _guiStyle.normal.textColor = Color.black;
        }

        private void Update()
        {
            float timeRemaining = RoundTimer.TimeRemaining;

            _displayText = string.Format("{0}:{1}{2}",
                Mathf.FloorToInt(Mathf.Max(timeRemaining / 60.0f, 0.0f)),
                Mathf.Max(timeRemaining % 60.0f) < 10.0f ? "0" : "",
                Mathf.FloorToInt(Mathf.Max(timeRemaining % 60.0f)));
        }

        private void OnGUI()
        {
            GUI.Label(_displayArea, _displayText, _guiStyle);
        }
    }
}
