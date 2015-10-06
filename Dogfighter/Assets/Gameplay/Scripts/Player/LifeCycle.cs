using UnityEngine;
using Gameplay.Scripts.Status.Messages;
using Gameplay.Scripts.Effects.Particles;

namespace Gameplay.Scripts.Player
{
    public class LifeCycle : MonoBehaviour
    {
        private Transform _planeModelTransform;
        private GameObject _planeModelObject;
        private Motion _planeMotionController;
        private ChaseCamera _chaseCamera;
        private MessageText _startMessagePopup;
        private RestartTimer _restartTimer;

        private void Awake()
        {
            Transform planeTransform = transform.FindChild("Plane");

            _planeModelTransform = planeTransform.FindChild("Plane Model");
            _planeModelObject = _planeModelTransform.gameObject;

            _planeMotionController = planeTransform.GetComponent<Motion>();
            _chaseCamera = transform.FindChild("Camera").GetComponent<ChaseCamera>();

            _startMessagePopup = transform.FindChild("Status Manager").FindChild("Messages").FindChild("Life Start").GetComponent<MessageText>();
            _restartTimer = transform.FindChild("Status Manager").FindChild("Messages").FindChild("Restart Timer").GetComponent<RestartTimer>();
        }

        private void Start()
        {
            Respawn();
        }

        public void LaunchDeathSequence()
        {
            _planeModelObject.SetActive(false);

            ExplosionPool.ActivateExplosion(_planeModelTransform.position);

            _restartTimer.Activate();
        }

        private void Update()
        {
            if ((!_planeModelObject.activeInHierarchy) && (_restartTimer.ReadyToRespawn))
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            _planeModelObject.SetActive(true);

            _planeMotionController.SetForNewLife();
            _chaseCamera.SetForNewLife();
            _startMessagePopup.Activate();
        }
    }
}
