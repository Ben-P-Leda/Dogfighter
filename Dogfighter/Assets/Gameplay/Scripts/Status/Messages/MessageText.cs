using UnityEngine;
using Shared.Scripts.Gui;

namespace Gameplay.Scripts.Status.Messages
{
    public class MessageText : GuiText
    {
        private float _remainingDuration;
        private bool _initialized;

        public float Duration;

        public virtual void Activate()
        {
            gameObject.SetActive(true);

            if (!_initialized) { SetUpDisplay(); }

            GuiStyle.normal.textColor = Color.black;
            _remainingDuration = Duration;
        }

        protected virtual void Awake()
        {
            _initialized = false;
        }

        public void SetUpDisplay()
        {
            StatusDisplayManager manager = transform.parent.parent.GetComponent<StatusDisplayManager>();
            base.SetUpDisplay(manager.ViewportScreenArea, manager.Scaling);
        }

        private void FixedUpdate()
        {
            if (_remainingDuration > 0.0f)
            {
                _remainingDuration -= Time.deltaTime;
            }
            else if (GuiStyle.normal.textColor.a > 0.0f)
            {
                GuiStyle.normal.textColor = new Color(0.0f, 0.0f, 0.0f, GuiStyle.normal.textColor.a - Time.deltaTime);
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
