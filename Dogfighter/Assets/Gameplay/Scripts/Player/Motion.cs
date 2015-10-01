using UnityEngine;

namespace Gameplay.Scripts.Player
{
    public class Motion : MonoBehaviour
    {
        private Transform _transform;
        private Terrain _terrain;
        private Transform _modelTransform;
        private string _playerId;

        private float _engineSpeed;
        private float _airSpeed;
        private float _bankingRoll;
        private bool _outOfControl;

        private void Awake()
        {
            _playerId = transform.parent.tag;

            _transform = transform;
            _terrain = Terrain.activeTerrain;

            _modelTransform = _transform.FindChild("Plane Model").transform;

            _engineSpeed = 50.0f;
            _airSpeed = 50.0f;
            _bankingRoll = 0.0f;
            _outOfControl = false;
        }

        private void FixedUpdate()
        {
            SetAirSpeed();
            SetInControlState();

            Vector2 controlValues = GetControlValues();

            UpdateDirection(controlValues);
            UpdateRollForBanking(controlValues);
            UpdatePosition();
        }

        private void SetAirSpeed()
        {
            float gravityEffectValue = CalculateGravitySlowDownValue();
            float excessAltitudeEffectValue = CalculateExcessAltitudeSlowDownValue();

            float maximumSpeed = Mathf.Max(0.0f, _engineSpeed + gravityEffectValue + excessAltitudeEffectValue);

            _airSpeed = Mathf.Clamp(_airSpeed + (Base_Acceleration * Time.deltaTime), 0.0f, maximumSpeed);
        }

        private float CalculateGravitySlowDownValue()
        {
            float pitch = _transform.localRotation.eulerAngles.x;
            float axisAngle = (pitch >= 180.0f) ? 270.0f : 90.0f;
            float direction = (pitch >= 180.0f) ? -1.0f : 1.0f;

            return (90.0f - Mathf.Abs(axisAngle - pitch)) * direction * Motion_Drag;
        }

        private float CalculateExcessAltitudeSlowDownValue()
        {
            return (_transform.position.y > Maximum_Altitude)
                ? (_transform.position.y - Maximum_Altitude) * Excess_Altitude_Drag
                : 0.0f;
        }

        private void SetInControlState()
        {
            if ((_transform.position.y > Maximum_Altitude) || (_airSpeed < Minimum_Flying_Speed))
            {
                _outOfControl = true;
            }
            else if ((_outOfControl) && (_airSpeed > Control_Regain_Speed))
            {
                _outOfControl = false;
            }
        }

        private Vector2 GetControlValues()
        {
            return _outOfControl 
                ? new Vector2(0.0f, 1.0f)
                : new Vector2(Input.GetAxis(_playerId + " Horizontal"), Input.GetAxis(_playerId + " Vertical"));
        }

        private void UpdateDirection(Vector2 controlValues)
        {
            float rollNegation = -_transform.localRotation.eulerAngles.z;

            _transform.Rotate(controlValues.y, controlValues.x, rollNegation);
        }

        private void UpdateRollForBanking(Vector2 controlValues)
        {
            if (controlValues.x != 0.0f)
            {
                float targetBankingRoll = Mathf.Abs(controlValues.x * Maximum_Banking_Roll);
                _bankingRoll = Mathf.Clamp(_bankingRoll - (2.0f * Banking_Roll_Speed * controlValues.x), -targetBankingRoll, targetBankingRoll);
            }
            else if (Mathf.Abs(_bankingRoll) < Banking_Roll_Speed)
            {
                _bankingRoll = 0.0f;
            }
            else
            {
                _bankingRoll -= Mathf.Sign(_bankingRoll) * Banking_Roll_Speed;
            }

            _modelTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, _bankingRoll);
        }

        private void UpdatePosition()
        {
            _transform.position += _transform.forward * Time.deltaTime * _airSpeed;

            float terrainHeight = _terrain.SampleHeight(transform.position);
            if (terrainHeight > _transform.position.y)
            {
                _transform.position = new Vector3(_transform.position.x, terrainHeight, _transform.position.z);
            }
        }

        private const float Maximum_Banking_Roll = 75.0f;
        private const float Banking_Roll_Speed = 2.0f;
        private const float Engine_Speed = 50.0f;
        private const float Base_Acceleration = 20.0f;
        private const float Motion_Drag = 1.0f;
        private const float Maximum_Altitude = 100.0f;
        private const float Excess_Altitude_Drag = 5.0f;
        private const float Minimum_Flying_Speed = 1.0f;
        private const float Control_Regain_Speed = 40.0f;
    }
}
