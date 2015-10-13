using UnityEngine;
using Gameplay.Scripts.PlayingField;

namespace Gameplay.Scripts.Target
{
    public class Motion : MonoBehaviour
    {
        private Transform _transform;
        private Vector3 _waypoint;

        private void Awake()
        {
            _transform = transform;
        }

        public void StartMovement()
        {
            SetStartPosition();
            SetWaypoint();

            _transform.LookAt(_waypoint);
        }

        private void SetStartPosition()
        {
            float startX = (Random.value > 0.5f) ? Field_Margin : Field_Dimensions - Field_Margin;
            float startZ = (Random.value > 0.5f) ? Field_Margin : Field_Dimensions - Field_Margin;

            if (Random.value > 0.5f) { startX = Random.Range(Field_Margin, Field_Dimensions - Field_Margin); }
            else { startZ = Random.Range(Field_Margin, Field_Dimensions - Field_Margin); }

            _transform.position = new Vector3(startX, RandomHeight(startX, startZ), startZ);
        }

        private float RandomHeight(float x, float z)
        {
            float fieldHeight = TerrainGrid.TerrainHeightAtPosition(new Vector3(x, Field_Height, z));

            return Random.Range(fieldHeight + Field_Margin, Field_Height - Field_Margin);
        }

        private void SetWaypoint()
        {
            float x = Random.Range(Field_Margin, Field_Dimensions - Field_Margin);
            float z = Random.Range(Field_Margin, Field_Dimensions - Field_Margin);

            _waypoint = new Vector3(x, RandomHeight(x, z), z);
        }

        private void FixedUpdate()
        {
            _transform.position += _transform.forward * Time.deltaTime * Speed;

            float minimumAltitude = TerrainGrid.TerrainHeightAtPosition(_transform.position) + Field_Margin;
            if (minimumAltitude > _transform.position.y)
            {
                _transform.position = new Vector3(_transform.position.x, minimumAltitude, _transform.position.z);
            }

            _transform.LookAt((_transform.position + _transform.forward) * 0.95f + (_waypoint * 0.05f));

            if (Vector3.Distance(_transform.position, _waypoint) < Waypoint_Achieved_Proximity) { SetWaypoint(); }
        }

        private const float Field_Dimensions = 1500.0f;
        private const float Field_Height = 100.0f;
        private const float Field_Margin = 10.0f;

        private const float Speed = 40.0f;
        private const float Waypoint_Achieved_Proximity = 5.0f;
    }
}
