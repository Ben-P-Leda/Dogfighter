using UnityEngine;
using Shared.Scripts;
using Shared.Scripts.GameData;
using Gameplay.Scripts.Effects.Particles;

namespace Gameplay.Scripts.Target
{
    public class Collisions : MonoBehaviour
    {
        private static Collisions _instance;
        public static void InflictDamage(string sourceId) { _instance.TakeDamage(sourceId); }

        private GameObject _targetModelObject;

        private int _remainingHitPoints;        

        private void Awake()
        {
            _instance = this;
            _targetModelObject = transform.FindChild("Target Model").gameObject;

            _remainingHitPoints = Hit_Points;
        }

        private void TakeDamage(string attackingPlayerId)
        {
            _remainingHitPoints--;

            if (_remainingHitPoints <= 0)
            {
                ExplosionPool.ActivateExplosion(_targetModelObject.transform.position);
                SoundEffectPlayer.PlaySound("explosion");

                _targetModelObject.SetActive(false);
                CurrentGame.Players[attackingPlayerId].TargetKills++;
            }
        }

        private const int Hit_Points = 5;
    }
}