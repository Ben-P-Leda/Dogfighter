using UnityEngine;
using Gameplay.Scripts.GameControl;
using Gameplay.Scripts.PlayingField;

namespace Gameplay.Scripts.Player
{
    public class Collisions : MonoBehaviour
    {
        private Transform _transform;
        private Transform _modelTransform;
        private string _playerId;

        public bool OnGround { get; private set; }
        public bool Crashed { get; private set; }

        private void Awake()
        {
            _playerId = transform.parent.tag;
            _transform = transform;
            _modelTransform = _transform.FindChild("Plane Model").transform;
        }

        private void FixedUpdate()
        {
            if (!Crashed)
            {
                HandleGroundImpacts();
            }
        }

        private void HandleGroundImpacts()
        {
            HandleLanding();

            HandleCrash(Vector3.forward * Center_To_Nose_Distance);
            HandleCrash(_modelTransform.localRotation * Vector3.left * Center_To_Wingtip_Distance);
            HandleCrash(_modelTransform.localRotation * Vector3.right * Center_To_Wingtip_Distance);
        }

        private void HandleLanding()
        {
            float centerTerrainHeight = TerrainGrid.TerrainHeightAtPosition(_transform.position);

            OnGround = false;
            if (centerTerrainHeight + Landing_Altitude >= _transform.position.y)
            {
                float pitch = _transform.eulerAngles.x;
                float roll = _transform.eulerAngles.z;
                if ((pitch >= 0.0f) && (pitch <= Landing_Maximum_Pitch) && (Mathf.Abs(roll) < Landing_Maximum_Roll))
                {
                    OnGround = true;
                    _transform.position = new Vector3(_transform.position.x, centerTerrainHeight + Landing_Altitude, _transform.position.z);
                }
            }
        }

        private void HandleCrash(Vector3 offset)
        {
            if (!Crashed)
            {
                float offsetHeight = _transform.position.y + offset.y;
                float terrainHeightAtOffset = TerrainGrid.TerrainHeightAtPosition(_transform.position + offset);

                if (terrainHeightAtOffset > offsetHeight)
                {
                    float centerTerrainHeight = TerrainGrid.TerrainHeightAtPosition(_transform.position);
                    _transform.position = new Vector3(_transform.position.x, centerTerrainHeight, _transform.position.z);
                    PlayerDeathHandler.TriggerDeath(_playerId, "");
                }
            }
        }

        public void SetForNewLife()
        {
            _transform.localPosition = new Vector3(0.0f, Landing_Altitude - 0.001f, 0.0f);
            _transform.localRotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);

            OnGround = true;
            Crashed = false;
        }

        public void SetForDeath()
        {
            Crashed = true;
        }


        private void OnTriggerEnter(Collider collider)
        {
            if (!Crashed)
            {
                if ((collider.tag == "Structure") || (collider.tag.StartsWith("Player"))) { PlayerDeathHandler.TriggerDeath(_playerId, ""); }
            }
        }

        private const float Landing_Altitude = 1.05f;
        private const float Landing_Maximum_Pitch = 7.5f;
        private const float Landing_Maximum_Roll = 0.001f;
        private const float Center_To_Nose_Distance = 2.5f;
        private const float Center_To_Wingtip_Distance = 2.5f;
    }
}
