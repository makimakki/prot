using UnityEngine;
using UnityEngine.UI;

public class MapRenderer : MonoBehaviour
{
    public RawImage mapImage;
    public MapDisplay mapDisplay;

    void Start()
    {
        UpdateMapTexture();
    }

    public void UpdateMapTexture()
    {
        mapImage.texture = mapDisplay.mapTexture;
    }

    void Update()
    {
        UpdateMapTexture(); // 毎フレーム更新することで表示を反映
    }
}