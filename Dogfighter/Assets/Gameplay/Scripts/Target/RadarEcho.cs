using UnityEngine;
using Gameplay.Scripts.GameControl.Helpers;

namespace Gameplay.Scripts.Target
{
    public class RadarEcho : MonoBehaviour
    {
        private GameObject _targetModelObject;

        public RadarData Data { get; private set; }

        private void Awake()
        {
            _targetModelObject = transform.FindChild("Target Model").gameObject;

            Data = new RadarData("", transform, Color.white);
            Data.Visible = false;
        }

        private void FixedUpdate()
        {
            Data.Visible = _targetModelObject.activeInHierarchy;
        }
    }
}
