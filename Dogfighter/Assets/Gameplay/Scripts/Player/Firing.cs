using UnityEngine;

namespace Gameplay.Scripts.Player
{
    public class Firing : MonoBehaviour
    {
        private Transform _transform;
        private RaycastHit _raycastHit;

        public string PlayerId { private get; set; }

        public string RayHit;

        private void Awake()
        {
            _transform = transform;
            _raycastHit = new RaycastHit();
        }

        private void Start()
        {
            Debug.Log(gameObject.name + ": Fire control axisPrefix assigned = " + PlayerId);
        }

        private void FixedUpdate()
        {
            if (Input.GetAxis(PlayerId + " Fire1") > 0.0f)
            {
                Ray ray = new Ray(_transform.position + (_transform.forward * Gun_Offset), _transform.forward);
                bool raycastHasHit = Physics.Raycast(ray, out _raycastHit, Gun_Range);
                if (raycastHasHit) { RayHit = _raycastHit.collider.tag; }
            }
        }

        private const float Gun_Range = 1000.0f;
        private const float Gun_Offset = 3.0f;
    }
}