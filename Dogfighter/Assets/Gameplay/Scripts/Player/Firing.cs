using UnityEngine;
using Gameplay.Scripts.GameControl;
using Shared.Scripts;

namespace Gameplay.Scripts.Player
{
    public class Firing : MonoBehaviour
    {
        private Transform _transform;
        private RaycastHit _raycastHit;

        private string _playerId;
        private float _timeToNextShot;

        private void Awake()
        {
            _playerId = transform.parent.tag;

            _transform = transform;
            _raycastHit = new RaycastHit();

            _timeToNextShot = 0.0f;
        }

        private void FixedUpdate()
        {
            if ((Input.GetAxis(_playerId + " Fire1") > 0.0f) && (_timeToNextShot == 0.0f))
            {
                Ray ray = new Ray(_transform.position + (_transform.forward * Gun_Offset), _transform.forward);
                bool raycastHasHit = Physics.Raycast(ray, out _raycastHit, Gun_Range);

                if (raycastHasHit) 
                {
                    PlayerToPlayerDamage.InflictDamage(_raycastHit.collider.tag, 1.0f, _playerId);
                }

                _timeToNextShot = Time_Between_Shots;
                SoundEffectPlayer.PlaySound("gun");
            }

            _timeToNextShot = Mathf.Max(_timeToNextShot - Time.deltaTime, 0.0f);
        }

        private const float Gun_Range = 100.0f;
        private const float Gun_Offset = 3.0f;
        private const float Time_Between_Shots = 0.125f;
    }
}