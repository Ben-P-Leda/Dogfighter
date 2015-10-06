using UnityEngine;
using Gameplay.Scripts.GameControl;

namespace Gameplay.Scripts.Player
{
    public class Collisions : MonoBehaviour
    {
        private Transform _transform;
        private GameObject _modelObject;
        private string _playerId;

        private Terrain[][] _terrains;

        public bool OnGround { get; set; }

        private void Awake()
        {
            _playerId = transform.parent.tag;

            _transform = transform;
            _modelObject = _transform.FindChild("Plane Model").gameObject;

            CreateTerrainGrid();
        }

        private void CreateTerrainGrid()
        {
            int maxX = 0;
            int maxZ = 0;

            foreach (Terrain terrain in Terrain.activeTerrains)
            {
                maxX = Mathf.Max(TerrainGridPosition(terrain.transform.position.x), maxX);
                maxZ = Mathf.Max(TerrainGridPosition(terrain.transform.position.z), maxZ);
            }

            _terrains = new Terrain[maxX + 1][];
            for (int i = 0; i < maxX + 1; i++) { _terrains[i] = new Terrain[maxZ + 1]; }

            foreach (Terrain terrain in Terrain.activeTerrains)
            {
                _terrains[TerrainGridPosition(terrain.transform.position.x)][TerrainGridPosition(terrain.transform.position.z)] = terrain;
            }
        }

        private int TerrainGridPosition(float worldPosition)
        {
            return Mathf.FloorToInt(worldPosition / Terrain_Side_Length);
        }

        private void FixedUpdate()
        {
            if (!_modelObject.activeInHierarchy)
            {
                HandleGroundImpacts();
            }
        }

        private void HandleGroundImpacts()
        {
            float centerTerrainHeight = TerrainHeightAtPosition(_transform.position);
            float noseTerrainHeight = TerrainHeightAtPosition(_transform.position + (Vector3.forward * Center_To_Nose_Distance));

            OnGround = false;
            if (centerTerrainHeight + Landing_Altitude >= _transform.position.y)
            {
                float pitch = _transform.eulerAngles.x;
                float roll = _transform.eulerAngles.z;
                if ((pitch >= 0.0f) && (pitch <= Landing_Maximum_Pitch) && (roll == 0.0f))
                {
                    OnGround = true;
                    _transform.position = new Vector3(_transform.position.x, centerTerrainHeight + Landing_Altitude, _transform.position.z);
                }
            }

            if (noseTerrainHeight > _transform.position.y)
            {
                _transform.position = new Vector3(_transform.position.x, centerTerrainHeight, _transform.position.z);

                PlayerDeathHandler.TriggerDeath(_playerId, "");
            }
        }

        private float TerrainHeightAtPosition(Vector3 position)
        {
            Terrain activeTerrain = _terrains[TerrainGridPosition(position.x)][TerrainGridPosition(position.z)];
            return activeTerrain.SampleHeight(position);
        }

        private const float Terrain_Side_Length = 500.0f;
        private const float Landing_Altitude = 1.05f;
        private const float Landing_Maximum_Pitch = 5.0f;
        private const float Center_To_Nose_Distance = 2.5f;
    }
}
