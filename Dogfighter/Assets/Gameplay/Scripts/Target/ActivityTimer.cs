using UnityEngine;

namespace Gameplay.Scripts.Target
{
    public class ActivityTimer : MonoBehaviour
    {
        private Transform _transform;
        private GameObject _targetModelObject;

        private float _timeToNextLaunch;
        private bool _currentlyActive;

        private void Awake()
        {
            _transform = transform;

            _targetModelObject = _transform.FindChild("Target Model").gameObject;
            _targetModelObject.SetActive(false);

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
            float startX = (Random.value > 0.5f) ? Field_Margin : Field_Dimensions - Field_Margin;
            float startY = (Random.Range(Field_Margin, Field_Height - Field_Margin));
            float startZ = (Random.value > 0.5f) ? Field_Margin : Field_Dimensions - Field_Margin;

            if (Random.value > 0.5f) { startX = Random.Range(Field_Margin, Field_Dimensions - Field_Margin); }
            else { startZ = Random.Range(Field_Margin, Field_Dimensions - Field_Margin); }

            _transform.position = new Vector3(startX, startY, startZ);
            _targetModelObject.SetActive(true);
            _currentlyActive = true;
        }

        private const float Time_To_First_Launch = 5.0f;
        private const float Field_Dimensions = 1500.0f;
        private const float Field_Height = 100.0f;
        private const float Field_Margin = 10.0f;
    }
}