using UnityEngine;
using Shared.Scripts;

namespace Gameplay.Scripts.Status.Messages
{
    public class MessageText : MonoBehaviour
    {
        private Rect _textArea;
        private GUIStyle _guiStyle;
        private float _remainingDuration;
        private bool _initialized;

        public Font Font;
        public string Text;
        public float Duration;

        public virtual void Activate()
        {
            gameObject.SetActive(true);

            if (!_initialized) { SetUpDisplay(); }

            _guiStyle.normal.textColor = Color.black;
            _remainingDuration = Duration;
        }

        protected virtual void Awake()
        {
            _initialized = false;
        }

        private void SetUpDisplay()
        {
            StatusDisplayManager manager = transform.parent.parent.GetComponent<StatusDisplayManager>();

            _textArea = manager.ViewportScreenArea;

            _guiStyle = new GUIStyle();
            _guiStyle.font = Font;
            _guiStyle.fontSize = (int)(26 * manager.Scaling);
            _guiStyle.wordWrap = true;
            _guiStyle.alignment = TextAnchor.MiddleCenter;
            _guiStyle.normal.textColor = Color.black;

            _initialized = true;
        }

        private void OnGUI()
        {
            GUI.Label(_textArea, Text, _guiStyle);
        }

        private void FixedUpdate()
        {
            if (_remainingDuration > 0.0f)
            {
                _remainingDuration -= Time.deltaTime;
            }
            else if (_guiStyle.normal.textColor.a > 0.0f)
            {
                _guiStyle.normal.textColor = new Color(0.0f, 0.0f, 0.0f, _guiStyle.normal.textColor.a - Time.deltaTime);
            }
            else
            {
                Deactivate();
            }
        }

        protected virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
