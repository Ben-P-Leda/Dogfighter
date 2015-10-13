using UnityEngine;
using Gameplay.Scripts.GameControl;
using Shared.Scripts;

namespace Gameplay.Scripts.Player
{
    public class Firing : MonoBehaviour
    {
        private string _playerId;
        private Transform _transform;
        private Collisions _collisionController;

        private RaycastHit _raycastHit;

        private float _timeToNextShot;
        public string _targetTag;

        private void Awake()
        {
            _playerId = transform.parent.tag;
            _transform = transform;
            _collisionController = GetComponent<Collisions>();

            _raycastHit = new RaycastHit();

            _timeToNextShot = 0.0f;
            _targetTag = "";
        }

        private void FixedUpdate()
        {
            CheckForLockOn();
            HandleFiring();
        }

        private void CheckForLockOn()
        {
            _targetTag = "";

            Ray ray = new Ray(_transform.position + (_transform.forward * Gun_Offset), _transform.forward);
            bool raycastHasHit = Physics.Raycast(ray, out _raycastHit, Gun_Range);

            if (raycastHasHit)
            {
                _targetTag = _raycastHit.collider.tag;
            }
        }

        private void HandleFiring()
        {
            if ((Input.GetAxis(_playerId + " Fire1") > 0.0f) && (_timeToNextShot == 0.0f) && (!_collisionController.OnGround))
            {
                _timeToNextShot = Time_Between_Shots;
                SoundEffectPlayer.PlaySound("gun");

                if (!string.IsNullOrEmpty(_targetTag))
                {
                    if (_targetTag == "Target")
                    {
                        Target.Collisions.InflictDamage(_playerId);
                    }
                    else
                    {
                        PlayerToPlayerDamage.InflictDamage(_targetTag, 5.0f, _playerId);
                    }
                }
            }

            _timeToNextShot = Mathf.Max(_timeToNextShot - Time.deltaTime, 0.0f);
        }

        private const float Gun_Range = 200.0f;
        private const float Gun_Offset = 3.0f;
        private const float Time_Between_Shots = 0.125f;
    }
}