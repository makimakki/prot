using UnityEngine;
using System.IO;

public class ExportTerrainToCSV : MonoBehaviour
{
    public Terrain terrain;
    private string filePath = "Assets/terrainHeights.bin";

    void Start()
    {
        ExportHeightsToBinary();

    }

    void ExportHeightsToBinary()
    {
        TerrainData terrainData = terrain.terrainData;
        float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            for (int y = 0; y < terrainData.heightmapResolution; y++)
            {
                for (int x = 0; x < terrainData.heightmapResolution; x++)
                {
                    writer.Write(heights[x, y]);
                }
            }
        }
        //terrainData.heightmapResolution = 513;
        Debug.Log("Terrain heights exported to " + filePath);

    }
}