using UnityEngine;

namespace Shared.Scripts.Gui
{
    public class GuiText : MonoBehaviour
    {
        private Rect _textArea;

        protected GUIStyle GuiStyle { get; private set; }

        public Font Font;
        public int FontSize;
        public string Text;
        public Rect DisplayArea;

        protected void SetUpDisplay(Rect viewportScreenArea, float scaling)
        {
            _textArea = new Rect(
                viewportScreenArea.x + viewportScreenArea.width * DisplayArea.x,
                (Screen.height - viewportScreenArea.height) - (viewportScreenArea.y - viewportScreenArea.height * DisplayArea.y),
                viewportScreenArea.width * DisplayArea.width,
                viewportScreenArea.height * DisplayArea.height);

            Debug.Log(_textArea.ToString());

            GuiStyle = new GUIStyle();
            GuiStyle.font = Font;
            GuiStyle.fontSize = (int)(FontSize * scaling);
            GuiStyle.wordWrap = true;
            GuiStyle.alignment = TextAnchor.MiddleCenter;
            GuiStyle.normal.textColor = Color.black;
        }

        private void OnGUI()
        {
            GUI.Label(_textArea, Text, GuiStyle);
        }
    }
}
