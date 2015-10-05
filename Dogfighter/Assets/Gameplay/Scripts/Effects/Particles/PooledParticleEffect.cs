using UnityEngine;

namespace Gameplay.Scripts.Effects.Particles
{
    public class PooledParticleEffect : MonoBehaviour
    {
        private Transform _transform = null;
        private ParticleSystem _particleSystem = null;

        public bool IsActive { get { return _particleSystem.isPlaying; } }

        private void Awake()
        {
            _transform = transform;
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void Activate(Vector3 position)
        {
            _transform.position = position;
            _particleSystem.Play();
        }
    }
}