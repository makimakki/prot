using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    public int width = 256; // Terrainの幅
    public int height = 256; // Terrainの奥行き
    private float scale = 0.1f; // Perlinノイズのスケール
    public float maxHeight = 280f; // Terrainの最大高さ（初期の260に最大の高さを足した値）

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData =  GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        //terrainData.heightmapResolution = 1024; // Heightmapの解像度を1024に設定
        //terrainData.size = new Vector3(width, maxHeight, height); // Terrainのサイズを設定
        terrainData.SetHeights(0, 0, GenerateHeights(terrainData));
        return terrainData;
    }

    float[,] GenerateHeights(TerrainData terrainData)
    {
        int resolution = terrainData.heightmapResolution;
        float[,] heights = new float[resolution, resolution];
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                heights[x, y] = CalculateHeight2(x, y);
            }
        }
        return heights;
    }


    float CalculateHeight3(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;
        float randomValue = Random.Range(-0.1f, 0.2f);
        //return 100 + Mathf.PerlinNoise(xCoord, yCoord);
        return (float)(100f - randomValue) / 250f;//0.1638f;
    }

    float CalculateHeight2(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;
         //return 100 + Mathf.PerlinNoise(xCoord, yCoord);
        return Mathf.PerlinNoise(xCoord, yCoord)  * 100/250;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;
        float baseHeight = 260f / maxHeight; // 初期の高さ260をmaxHeightに対して正規化
        return baseHeight + Mathf.PerlinNoise(xCoord, yCoord) * ((maxHeight - 260f) / maxHeight);
    }
}