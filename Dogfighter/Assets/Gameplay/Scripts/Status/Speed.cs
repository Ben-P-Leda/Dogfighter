using UnityEngine;
using System.Collections.Generic;
using Shared.Scripts;
using Gameplay.Scripts.Player;
using Gameplay.Scripts.Display;

namespace Gameplay.Scripts.Status
{
    public class Speed : MonoBehaviour
    {
        private Player.Motion _planeMotionScript;

        private Rect _displayArea;
        private Vector2 _center;

        public Texture BaseImage;
        public Texture NeedleImage;

        public float col;

        private void Awake()
        {
            _planeMotionScript = transform.parent.parent.FindChild("Plane").GetComponent<Player.Motion>();
        }

        private void Start()
        {
            SetUpDisplay();
        }

        private void SetUpDisplay()
        {
            SplitScreenDisplayManager manager = transform.parent.parent.GetComponent<SplitScreenDisplayManager>();

            _displayArea = manager.ScaleToDisplay(BaseImage.width, BaseImage.height, TextAnchor.LowerRight, 30.0f, 30.0f);
            _center = new Vector2(_displayArea.x + (_displayArea.width * 0.5f), _displayArea.y + (_displayArea.height * 0.5f));
        }

        private void OnGUI()
        {
            GUI.DrawTexture(_displayArea, BaseImage);

            GUIUtility.RotateAroundPivot(_planeMotionScript.EngineSpeedFraction * Maximum_Needle_Rotation, _center);
            GUI.DrawTexture(_displayArea, NeedleImage);
            GUIUtility.RotateAroundPivot(0.0f, _center);
        }

        private const float Maximum_Needle_Rotation = 270.0f;
    }
}
