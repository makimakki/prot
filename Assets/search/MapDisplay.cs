using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Texture2D mapTexture;

    void Start()
    {
        mapTexture = new Texture2D(100, 100); // 地図のサイズ
        for (int y = 0; y < mapTexture.height; y++)
        {
            for (int x = 0; x < mapTexture.width; x++)
            {
                mapTexture.SetPixel(x, y, Color.white);
            }
        }
        mapTexture.Apply();
    }
}