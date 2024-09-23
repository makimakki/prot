using UnityEngine;

public class SpriteGlowEffect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
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

        // スプライトのカラーを設定する（光る効果をシミュレート）
        Color newColor = spriteRenderer.color;
        newColor.a = currentGlow; // アルファ値（透明度）を使用して光の強さを調整
        spriteRenderer.color = newColor;
    }
}