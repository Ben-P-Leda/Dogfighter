using UnityEngine;
using Shared.Scripts;

namespace Gameplay.Scripts.Effects.Particles
{
    public sealed class ExplosionPool : GenericObjectPool<PooledParticleEffect>
    {
        private static ExplosionPool _instance = null;
        public static PooledParticleEffect ActivateExplosion(Vector3 position) { return _instance.ActivateExplosionAt(position); }

        protected override void Awake()
        {
            base.Awake();

            if (_instance == null) { _instance = this; }
        }

        protected override bool ObjectIsAvailable(PooledParticleEffect objectToCheck)
        {
            return !objectToCheck.IsActive;
        }

        private PooledParticleEffect ActivateExplosionAt(Vector3 position)
        {
            PooledParticleEffect Explosion = GetFirstAvailableObject();
            if (Explosion != null) { Explosion.Activate(position); }

            return Explosion;
        }
    }
}