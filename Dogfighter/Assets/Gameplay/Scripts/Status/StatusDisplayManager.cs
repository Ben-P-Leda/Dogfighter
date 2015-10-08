using UnityEngine;
using Gameplay.Scripts.Status.Messages;

namespace Gameplay.Scripts.Status
{
    public class StatusDisplayManager : MonoBehaviour
    {
        private bool _initialized;
        private Rect _viewportScreenArea;

        private Health _healthController;

        private MessageText _startMessagePopup;
        private RestartTimer _restartTimer;

        public string PlayerId { get; private set; }
        public float Scaling { get; private set; }
        public bool ReadyToRespawn { get { return _restartTimer.ReadyToRespawn; } }

        public Rect ViewportScreenArea 
        {  
            get
            {
                if (!_initialized) { Initialize(); }
                return _viewportScreenArea;
            }
        }

        private void Awake()
        {
            PlayerId = transform.parent.tag;

            _healthController = transform.FindChild("Health").GetComponent<Health>();

            _startMessagePopup = transform.FindChild("Messages").FindChild("Life Start").GetComponent<MessageText>();
            _restartTimer = transform.FindChild("Messages").FindChild("Restart Timer").GetComponent<RestartTimer>();

            _initialized = false;
        }

        private void Initialize()
        {
            Scaling = Screen.height / One_To_One_Screen_Height;

            GetViewportScreenArea();

            _initialized = true;
        }

        private void GetViewportScreenArea()
        {
            Camera camera = transform.parent.FindChild("Camera").GetComponent<Camera>();

            Vector3 topLeft = camera.ViewportToScreenPoint(Vector3.zero);
            Vector3 bottomRight = camera.ViewportToScreenPoint(new Vector3(1.0f, 1.0f, 0.0f));

            _viewportScreenArea = new Rect(topLeft.x, topLeft.y, bottomRight.x - topLeft.x, bottomRight.y - topLeft.y);
        }

        public Rect ScaleToDisplay(float width, float height, TextAnchor alignment, float horizontalMargin, float verticalMargin)
        {
            if (!_initialized) { Initialize(); }

            horizontalMargin *= Scaling;
            verticalMargin *= Scaling;
            width *= Scaling;
            height *= Scaling;

            float left = _viewportScreenArea.x + ((_viewportScreenArea.width - width) / 2.0f);
            float top = _viewportScreenArea.y + ((_viewportScreenArea.height - height) / 2.0f);

            if ((alignment == TextAnchor.LowerLeft) || (alignment == TextAnchor.MiddleLeft) || (alignment == TextAnchor.UpperLeft))
            {
                left = _viewportScreenArea.x + horizontalMargin;
            }
            else if ((alignment == TextAnchor.LowerRight) || (alignment == TextAnchor.MiddleRight) || (alignment == TextAnchor.UpperRight))
            {
                left = (_viewportScreenArea.x + _viewportScreenArea.width) - (width + horizontalMargin);
            }

            if ((alignment == TextAnchor.UpperLeft) || (alignment == TextAnchor.UpperCenter) || (alignment == TextAnchor.UpperRight))
            {
                top = _viewportScreenArea.y + verticalMargin;
            }
            else if ((alignment == TextAnchor.LowerLeft) || (alignment == TextAnchor.LowerCenter) || (alignment == TextAnchor.LowerRight))
            {
                top = (_viewportScreenArea.y + _viewportScreenArea.height) - (height + verticalMargin);
            }

            return new Rect(left, top, width, height);
        }

        public void SetForNewLife()
        {
            _healthController.SetForNewLife();
            _startMessagePopup.Activate();
        }

        public void SetForDeath()
        {
            _restartTimer.Activate();
        }

        private const float One_To_One_Screen_Height = 675.0f;
    }
}
