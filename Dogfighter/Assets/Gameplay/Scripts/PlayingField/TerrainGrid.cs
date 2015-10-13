using UnityEngine;

namespace Gameplay.Scripts.PlayingField
{
    public class TerrainGrid : MonoBehaviour
    {
        private static TerrainGrid _instance;
        public static float TerrainHeightAtPosition(Vector3 position) { return _instance.GetTerrainHeight(position); }

        private Terrain[][] _terrains;

        private void Awake()
        {
            _instance = this;

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

        private float GetTerrainHeight(Vector3 position)
        {
            Terrain activeTerrain = _terrains[TerrainGridPosition(position.x)][TerrainGridPosition(position.z)];
            return activeTerrain.SampleHeight(position);
        }

        private const float Terrain_Side_Length = 500.0f;
    }
}
