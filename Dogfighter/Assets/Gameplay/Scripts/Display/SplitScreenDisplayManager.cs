using UnityEngine;

namespace Gameplay.Scripts.Display
{
    public class SplitScreenDisplayManager : MonoBehaviour
    {
        private bool _initialized;
        private Rect _viewportScreenArea;
        private GUIStyle _guiStyle;

        public string PlayerId { get; private set; }
        public float Scaling { get; private set; }

        public Rect ViewportScreenArea 
        {  
            get
            {
                if (!_initialized) { Initialize(); }
                return _viewportScreenArea;
            }
        }

        public GUIStyle GuiStyle
        {
            get
            {
                if (!_initialized) { Initialize(); }
                return _guiStyle;
            }
        }

        private void Awake()
        {
            PlayerId = transform.parent.tag;

            _initialized = false;
        }

        private void Initialize()
        {
            Scaling = Screen.height / One_To_One_Screen_Height;

            GetViewportScreenArea();
            CreateCommonGuiStyle();

            _initialized = true;
        }

        private void GetViewportScreenArea()
        {
            Camera camera = transform.FindChild("Camera").GetComponent<Camera>();

            Vector3 topLeft = camera.ViewportToScreenPoint(Vector3.zero);
            Vector3 bottomRight = camera.ViewportToScreenPoint(new Vector3(1.0f, 1.0f, 0.0f));

            _viewportScreenArea = new Rect(topLeft.x, topLeft.y, bottomRight.x - topLeft.x, bottomRight.y - topLeft.y);
        }

        private void CreateCommonGuiStyle()
        {
            _guiStyle = new GUIStyle();
            _guiStyle.fontStyle = FontStyle.Bold;
            _guiStyle.fontSize = (int)(14 * Scaling);
            _guiStyle.wordWrap = true;
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
