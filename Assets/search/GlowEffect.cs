using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    public Material material;
    public float minGlow = 0f;
    public float maxGlow = 1f;
    public float glowSpeed = 1f;

    private float glowDirection = 1f;
    private float currentGlow = 0f;

    void Update()
    {
        // エミッションの強さを変更する
        currentGlow += glowSpeed * glowDirection * Time.deltaTime;
        
        // エミッションの強さが最小または最大に達した場合、方向を反転する
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

        // マテリアルのエミッションカラーを設定する
        Color emissionColor = material.color * Mathf.LinearToGammaSpace(currentGlow);
        material.SetColor("_EmissionColor", emissionColor);
    }
}