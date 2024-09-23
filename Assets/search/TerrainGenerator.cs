using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Terrain terrain;
    private TerrainData terrainData;

    void Start()
    {
        terrainData = terrain.terrainData;
        // 地形のスケールを調整
        terrainData.size = new Vector3(terrainData.size.x, 100f, terrainData.size.z);
        GenerateIsland();
    }
    void GenerateHeightMap()
    {
        int mapWidth = terrainData.heightmapResolution;
        int mapHeight = terrainData.heightmapResolution;
        float[,] heights = new float[mapWidth, mapHeight];

        float plateauRadius = 0.4f; // 平坦な部分の半径（0-1の範囲）
        float cliffWidth = 0.1f; // 崖の幅（0-1の範囲）
        float maxHeight = 0.5f; // 最大の高さ

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float xCoord = (float)x / mapWidth;
                float yCoord = (float)y / mapHeight;
                float distanceFromCenter = Vector2.Distance(new Vector2(xCoord, yCoord), new Vector2(0.5f, 0.5f));

                float height;
                if (distanceFromCenter <= plateauRadius)
                {
                    // 中心部分は平坦
                    height = maxHeight;
                }
                else if (distanceFromCenter <= plateauRadius + cliffWidth)
                {
                    // 崖の部分
                    float t = (distanceFromCenter - plateauRadius) / cliffWidth;
                    height = Mathf.Lerp(maxHeight, 0, t * t); // 二次関数で急激に下降
                }
                else
                {
                    // 外側は完全に平坦（海面レベル）
                    height = 0;
                }

                heights[x, y] = height;
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }
    void SmoothTerrain()
    {
        float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution;

        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                float averageHeight = (
                    heights[x - 1, y] + heights[x + 1, y] +
                    heights[x, y - 1] + heights[x, y + 1]
                ) / 4f;

                heights[x, y] = Mathf.Lerp(heights[x, y], averageHeight, 0.5f);
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }

    void GenerateIsland()
    {
        GenerateHeightMap();
        SmoothTerrain();
        // 必要に応じて複数回スムージングを適用
        for (int i = 0; i < 3; i++)
        {
            SmoothTerrain();
        }
    }
}