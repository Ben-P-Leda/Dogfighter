using UnityEngine;

namespace Gameplay.Scripts.Player
{
    public class ChaseCamera : MonoBehaviour
    {
        private Transform _transform;
        private Transform _targetTransform;

        public GameObject ChaseTarget;

        private void Awake()
        {
            _transform = transform;
            _targetTransform = ChaseTarget.transform;
        }

        private void FixedUpdate()
        {
            Vector3 focalTarget = _targetTransform.position - _targetTransform.forward * 10.0f + Vector3.up * 5.0f;

            _transform.position = (_transform.position * (1.0f - Bias_Toward_Focal_Target))
                                    + (focalTarget * Bias_Toward_Focal_Target);

            _transform.LookAt(_targetTransform.position + _targetTransform.forward * 30.0f);
        }

        private const float Bias_Toward_Focal_Target = 0.05f;
        private const float Downward_Pan_Limit = 75.0f;
    }
}