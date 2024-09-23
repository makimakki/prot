using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MapDisplay mapDisplay;
    public MapRenderer mapRenderer;
    public Transform player;
    public float revealRadius = 5.0f;

    void Update()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.z);
        RevealMap(playerPos);
        mapRenderer.UpdateMapTexture(); // 地図の更新を反映
    }

    void RevealMap(Vector2 position)
    {
        int centerX = Mathf.RoundToInt(position.x);
        int centerY = Mathf.RoundToInt(position.y);

        for (int y = centerY - Mathf.RoundToInt(revealRadius); y <= centerY + Mathf.RoundToInt(revealRadius); y++)
        {
            for (int x = centerX - Mathf.RoundToInt(revealRadius); x <= centerX + Mathf.RoundToInt(revealRadius); x++)
            {
                if (x >= 0 && x < mapDisplay.mapTexture.width && y >= 0 && y < mapDisplay.mapTexture.height)
                {
                    mapDisplay.mapTexture.SetPixel(x, y, Color.black);
                }
            }
        }
        mapDisplay.mapTexture.Apply();
    }
}