using UnityEngine;

namespace Gameplay.Scripts.Target
{
    public class ActivityTimer : MonoBehaviour
    {
        private GameObject _targetModelObject;
        private Motion _motionController;

        private float _timeToNextLaunch;
        private bool _currentlyActive;

        private void Awake()
        {
            _targetModelObject = transform.FindChild("Target Model").gameObject;
            _targetModelObject.SetActive(false);

            _motionController = GetComponent<Motion>();

            ResetLaunchTimer();
        }

        private void FixedUpdate()
        {
            if (!_targetModelObject.activeInHierarchy)
            {
                if (_currentlyActive) { ResetLaunchTimer(); }
                else { DecreaseLaunchTimer(); }
            }
        }

        private void ResetLaunchTimer()
        {
            _timeToNextLaunch = Time_To_First_Launch;
            _currentlyActive = false;
        }

        private void DecreaseLaunchTimer()
        {
            _timeToNextLaunch -= Time.deltaTime;

            if (_timeToNextLaunch <= 0.0f) { Activate(); }
        }

        private void Activate()
        {
            _targetModelObject.SetActive(true);
            _motionController.StartMovement();

            _currentlyActive = true;
        }

        private const float Time_To_First_Launch = 5.0f;
    }
}