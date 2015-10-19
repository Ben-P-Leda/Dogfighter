using UnityEngine;
using System.Collections.Generic;
using Shared.Scripts;
using Gameplay.Scripts.GameControl.Helpers;

namespace Gameplay.Scripts.Status
{
    public class Radar : MonoBehaviour
    {
        private Transform _planeTransform;

        private Rect _displayArea;
        private float _radius;
        private Vector2 _center;
        private float _scaling;

        public RadarData[] ObjectsToTrack { private get; set; }

        public Texture BaseImage;
        public Texture BlipImage;

        private void Awake()
        {
            _planeTransform = transform.parent.parent.FindChild("Plane");
        }

        public void SetUpDisplay()
        {
            StatusDisplayManager manager = transform.parent.GetComponent<StatusDisplayManager>();
            _scaling = manager.Scaling;

            _displayArea = manager.ScaleToDisplay(BaseImage.width, BaseImage.height, TextAnchor.LowerLeft, 30.0f, 30.0f);
            _center = new Vector2(_displayArea.x + (_displayArea.width * 0.5f), _displayArea.y + (_displayArea.height * 0.5f));
            _radius = _displayArea.width * 0.45f;
        }

        private void OnGUI()
        {
            GUIUtility.RotateAroundPivot(_planeTransform.rotation.eulerAngles.y, _center);
            GUI.DrawTexture(_displayArea, BaseImage);
            GUIUtility.RotateAroundPivot(0.0f, _center);

            DrawBlips();
        }

        private void DrawBlips()
        {
            for (int i = 0; i < ObjectsToTrack.Length; i++)
            {
                if ((ObjectsToTrack[i].Visible) && (InRadarRange(ObjectsToTrack[i].Position)))
                {
                    DrawBlip(ObjectsToTrack[i].Position, ObjectsToTrack[i].Colour);
                }
            }
        }

        private bool InRadarRange(Vector3 objectPosition)
        {
            float xDistance = Mathf.Abs(_planeTransform.position.x - objectPosition.x);
            float zDistance = Mathf.Abs(_planeTransform.position.z - objectPosition.z);

            return ((xDistance * xDistance) + (zDistance * zDistance) < Maximum_Detection_Range * Maximum_Detection_Range);
        }

        private void DrawBlip(Vector3 objectPosition, Color blipColour)
        {
            Vector2 distanceFromCenter = new Vector2(_planeTransform.position.x - objectPosition.x, _planeTransform.position.z - objectPosition.z);
            distanceFromCenter = (distanceFromCenter / Maximum_Detection_Range) * _radius;

            float sideLength = BlipImage.width * _scaling * (1.0f - ((_planeTransform.position.y - objectPosition.y) * Altitude_Blip_Size_Modifier));

            Rect blipRect = new Rect(
                _center.x + distanceFromCenter.x - (sideLength * 0.5f),
                _center.y + distanceFromCenter.y - (sideLength * 0.5f),
                sideLength,
                sideLength);

            GUI.color = blipColour;
            GUI.DrawTexture(blipRect, BlipImage);
        }

        private float Maximum_Detection_Range = 1000.0f;
        private float Altitude_Blip_Size_Modifier = 0.005f;
    }
}
