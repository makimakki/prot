using UnityEngine;
using UnityEngine.UI;

public class RawImageYellowGlowEffect : MonoBehaviour
{
    public RawImage rawImage;
    public float minGlow = 0f;
    public float maxGlow = 1f;
    public float glowSpeed = 1f;

    private float glowDirection = 1f;
    private float currentGlow = 0f;

    void Update()
    {
        // 光の強さを変更する
        currentGlow += glowSpeed * glowDirection * Time.deltaTime;

        // 光の強さが最小または最大に達した場合、方向を反転する
        if (currentGlow >= maxGlow)
        {
            currentGlow = maxGlow;
            glowDirection = -1f;
        }
        else if (currentGlow <= minGlow)
        {
            currentGlow = minGlow;
            glowDirection = 1f;
        }

        // RawImageのカラーを黄色に設定する（光る効果をシミュレート）
        Color newColor = new Color(1f, 1f, 0f, currentGlow); // RGBを黄色に設定し、アルファ値を調整
        rawImage.color = newColor;
    }
}