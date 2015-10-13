using UnityEngine;

namespace Shared.Scripts.Gui
{
    public abstract class GuiManager : MonoBehaviour
    {
        private bool _initialized;
        private Rect _viewportScreenArea;

        public float Scaling { get; private set; }

        public Rect ViewportScreenArea
        {
            get
            {
                if (!_initialized) { Initialize(); }
                return _viewportScreenArea;
            }
        }

        protected virtual void Awake()
        {
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
            Camera camera = GetCamera();

            Vector3 topLeft = camera.ViewportToScreenPoint(Vector3.zero);
            Vector3 bottomRight = camera.ViewportToScreenPoint(new Vector3(1.0f, 1.0f, 0.0f));

            _viewportScreenArea = new Rect(topLeft.x, topLeft.y, bottomRight.x - topLeft.x, bottomRight.y - topLeft.y);
        }

        protected virtual Camera GetCamera()
        {
            return Camera.main;
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

        private const float One_To_One_Screen_Height = 675.0f;
    }
}
