using UnityEngine;
using System.IO;

public class ImportTerrainFromCSV : MonoBehaviour
{
    public Terrain terrain;
    //private string filePath = "Assets/terrainHeights.bin";
    private string filePath = "terrainHeights.bin";

    void Start()
    {
        ImportHeightsFromBinary();
    }

    void ImportHeightsFromBinary()
    {
        TerrainData terrainData = terrain.terrainData;
        int resolution = terrainData.heightmapResolution;
        float[,] heights = new float[resolution, resolution];

        TextAsset binaryFile = Resources.Load<TextAsset>(filePath);

        if (binaryFile != null)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(binaryFile.bytes)))
            {
                for (int y = 0; y < resolution; y++)
                {
                    for (int x = 0; x < resolution; x++)
                    {
                        heights[x, y] = reader.ReadSingle();
                    }
                }
            }
        }

        // using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        // {
        //     for (int y = 0; y < resolution; y++)
        //     {
        //         for (int x = 0; x < resolution; x++)
        //         {
        //             heights[x, y] = reader.ReadSingle();
        //         }
        //     }
        // }

        terrainData.SetHeights(0, 0, heights);
        Debug.Log("Terrain heights imported from " + filePath);
    }
}