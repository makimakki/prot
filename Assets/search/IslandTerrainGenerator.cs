using UnityEngine;

public class IslandTerrainGenerator : MonoBehaviour
{
    private int width = 256;
    private int height = 256;
    private float depth = 20f;
    private float islandFalloff = 2.0f;
    private float cliffDistance = 1.5f;

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights(terrainData));
        return terrainData;
    }

    float[,] GenerateHeights(TerrainData terrainData)
    {
        float[,] heights = new float[width, height];
        float maxDistance = Mathf.Min(width, height) / 2f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float distance = CalculateDistanceToCenter(x, y);
                float falloff = CalculateFalloff(terrainData, distance, maxDistance);

                heights[x, y] = Mathf.Clamp01(1 - falloff) * depth;
            }
        }
        return heights;
    }

    float CalculateDistanceToCenter(int x, int y)
    {
        float centerX = width / 2f;
        float centerY = height / 2f;
        return Mathf.Sqrt(Mathf.Pow(x - centerX, 2) + Mathf.Pow(y - centerY, 2));
    }

    float CalculateFalloff(TerrainData terrainData, float distance, float maxDistance)
    {
        float cliffStart = maxDistance - (cliffDistance / terrainData.size.x * width);
        if (distance > cliffStart)
        {
            return Mathf.Pow((distance - cliffStart) / (maxDistance - cliffStart), islandFalloff);
        }
        return 0;
    }
}