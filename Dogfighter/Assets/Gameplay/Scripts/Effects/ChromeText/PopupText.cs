using UnityEngine;
using Shared.Scripts;
using Gameplay.Scripts.Display;

namespace Gameplay.Scripts.Effect.ChromeText
{
    public class PopupText : MonoBehaviour
    {
        private Rect _textArea;
        private GUIStyle _guiStyle;

        public Font Font;

        private void Awake()
        {
        }

        private void Start()
        {
            SetUpDisplay();
        }

        private void SetUpDisplay()
        {
            SplitScreenDisplayManager manager = transform.parent.parent.GetComponent<SplitScreenDisplayManager>();

            _textArea = manager.ViewportScreenArea;

            _guiStyle = new GUIStyle();
            _guiStyle.font = Font;
            _guiStyle.fontSize = (int)(26 * manager.Scaling);
            _guiStyle.wordWrap = true;
            _guiStyle.alignment = TextAnchor.MiddleCenter;
        }

        private void OnGUI()
        {
            GUI.Label(_textArea, "Ready?", _guiStyle);
        }
    }
}
