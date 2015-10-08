using UnityEngine;
using Shared.Scripts;
using Gameplay.Scripts.Status;
using Gameplay.Scripts.Effects.Particles;

namespace Gameplay.Scripts.Player
{
    public class LifeCycle : MonoBehaviour
    {
        private Transform _modelTransform;
        private GameObject _modelObject;
        private Motion _motionController;
        private Collisions _collisionController;
        private ChaseCamera _chaseCameraController;
        private StatusDisplayManager _statusManager;
        

        private void Awake()
        {
            Transform planeTransform = transform.FindChild("Plane");

            _modelTransform = planeTransform.FindChild("Plane Model");
            _modelObject = _modelTransform.gameObject;

            _motionController = planeTransform.GetComponent<Motion>();
            _collisionController = planeTransform.GetComponent<Collisions>();
            _chaseCameraController = transform.FindChild("Camera").GetComponent<ChaseCamera>();

            _statusManager = transform.FindChild("Status Manager").GetComponent<StatusDisplayManager>();
        }

        private void Start()
        {
            Respawn();
        }

        public void LaunchDeathSequence()
        {
            _modelObject.SetActive(false);
            _statusManager.SetForDeath();
            _collisionController.SetForDeath();

            ExplosionPool.ActivateExplosion(_modelTransform.position);
            SoundEffectPlayer.PlaySound("explosion");
        }

        private void Update()
        {
            if ((!_modelObject.activeInHierarchy) && (_statusManager.ReadyToRespawn))
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            _modelObject.SetActive(true);

            _collisionController.SetForNewLife();
            _motionController.SetForNewLife();
            _chaseCameraController.SetForNewLife();
            _statusManager.SetForNewLife();
        }
    }
}
