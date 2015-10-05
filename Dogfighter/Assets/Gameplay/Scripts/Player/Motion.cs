using UnityEngine;

using Gameplay.Scripts.Effects.Particles;

namespace Gameplay.Scripts.Player
{
    public class Motion : MonoBehaviour
    {
        private Transform _transform;
        private Terrain _terrain;
        private GameObject _modelObject;
        private Transform _modelTransform;
        private string _playerId;

        private float _engineSpeed;
        private float _airSpeed;
        private float _bankingRoll;
        private bool _outOfControl;
        private bool _onGround;
        private bool _crashed;

        public float EngineSpeedFraction { get { return _engineSpeed / MaximumAirSpeed; } }

        public float Acceleration;
        public float MaximumAirSpeed;
        public float TurnSpeed;

        public float Speed;

        private void Awake()
        {
            _playerId = transform.parent.tag;

            _transform = transform;
            _terrain = Terrain.activeTerrain;

            _modelTransform = _transform.FindChild("Plane Model").transform;
            _modelObject = _modelTransform.gameObject;

            _engineSpeed = 1.0f;
            _airSpeed = 5.0f;
            _bankingRoll = 0.0f;
            _outOfControl = false;
            _onGround = true;
            _crashed = false;
        }

        private void FixedUpdate()
        {
            if (!_crashed)
            {
                SetAirSpeed();
                SetInControlState();

                Vector2 steeringControlValues = GetSteeringControlValues();

                UpdateDirection(steeringControlValues);
                UpdateRollForBanking(steeringControlValues);
                UpdatePosition();

                HandleGroundImpacts();
            }
        }

        private void SetAirSpeed()
        {
            _engineSpeed = Mathf.Clamp(_engineSpeed + (Input.GetAxis(_playerId + " Throttle") * Acceleration), 0.0f, MaximumAirSpeed);

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
            if (_onGround)
            {
                _outOfControl = false;
            }
            else if ((_transform.position.y > Maximum_Altitude) || (_airSpeed < Minimum_Flying_Speed))
            {
                _outOfControl = true;
            }
            else if ((_outOfControl) && (_airSpeed > Control_Regain_Speed))
            {
                _outOfControl = false;
            }
        }

        private Vector2 GetSteeringControlValues()
        {
            Vector2 controlValues;

            if (_onGround) { controlValues = new Vector2(0.0f, Mathf.Min(0.0f, Input.GetAxis(_playerId + " Vertical"))); }
            else if (_outOfControl) { controlValues = Vector2.up; }
            else { controlValues = new Vector2(Input.GetAxis(_playerId + " Horizontal"), Input.GetAxis(_playerId + " Vertical"));}

            return controlValues;
        }

        private void UpdateDirection(Vector2 controlValues)
        {
            float rollNegation = -_transform.localRotation.eulerAngles.z;
            float pitchRotation = controlValues.y * TurnSpeed;

            if (Falling())
            {
                pitchRotation = Mathf.Max(0.0f, pitchRotation);
            }
            _transform.Rotate(pitchRotation, controlValues.x * TurnSpeed, rollNegation);
        }

        private bool Falling()
        {
            return (_transform.localEulerAngles.y >= Maximum_Fall_Angle) && (_transform.localEulerAngles.y < 180.0f);
        }

        private void UpdateRollForBanking(Vector2 controlValues)
        {
            if (controlValues.x != 0.0f)
            {
                float targetBankingRoll = Mathf.Abs(controlValues.x * Maximum_Banking_Roll);
                _bankingRoll = Mathf.Clamp(_bankingRoll - (2.0f * Banking_Roll_Speed * controlValues.x * TurnSpeed), 
                    -targetBankingRoll, targetBankingRoll);
            }
            else if (Mathf.Abs(_bankingRoll) < Banking_Roll_Speed)
            {
                _bankingRoll = 0.0f;
            }
            else
            {
                _bankingRoll -= Mathf.Sign(_bankingRoll) * Banking_Roll_Speed * TurnSpeed;
            }

            _modelTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, _bankingRoll);
        }

        private void UpdatePosition()
        {
            _transform.position += _transform.forward * Time.deltaTime * _airSpeed;
        }

        private void HandleGroundImpacts()
        {
            float terrainHeight = _terrain.SampleHeight(transform.position);

            _onGround = false;
            if (terrainHeight + Landing_Altitude >= _transform.position.y)
            {
                float pitch = _transform.eulerAngles.x;
                float roll = _transform.eulerAngles.z;
                if ((pitch >= 0.0f) && (pitch <= Landing_Maximum_Pitch) && (roll == 0.0f))
                {
                    _onGround = true;
                    _transform.position = new Vector3(_transform.position.x, terrainHeight + Landing_Altitude, _transform.position.z);
                }
            }

            if (terrainHeight > _transform.position.y)
            {
                _crashed = true;
                _modelObject.SetActive(false);
                _transform.position = new Vector3(_transform.position.x, terrainHeight, _transform.position.z);

                ExplosionPool.ActivateExplosion(_modelTransform.position);
            }
        }

        private const float Maximum_Banking_Roll = 75.0f;
        private const float Banking_Roll_Speed = 8.0f;
        private const float Engine_Speed = 50.0f;
        private const float Base_Acceleration = 20.0f;
        private const float Motion_Drag = 1.0f;
        private const float Maximum_Altitude = 100.0f;
        private const float Excess_Altitude_Drag = 5.0f;
        private const float Minimum_Flying_Speed = 20.0f;
        private const float Control_Regain_Speed = 40.0f;
        private const float Maximum_Fall_Angle = 75.0f;

        private const float Landing_Altitude = 1.05f;
        private const float Landing_Maximum_Pitch = 5.0f;
    }
}
