using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlinkEffect : MonoBehaviour
{
    public RawImage rawImage;           // 変更対象のRawImageコンポーネント
    public Color blinkColor = Color.red; // ピカピカする色（赤）
    public float blinkSpeed = 1.0f;      // ブリンクの速度
    public float blinkDuration = 5.0f;   // ブリンクする期間

    private void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        float endTime = Time.time + blinkDuration;

        while (Time.time < endTime)
        {
            // 色の点滅効果
            float lerpFactor = Mathf.Abs(Mathf.Sin(Time.time * blinkSpeed));
            rawImage.color = Color.Lerp(Color.white, blinkColor, lerpFactor);

            yield return null;
        }

        // 色を元に戻す
        rawImage.color = Color.white;
    }
}
// using UnityEngine;
// using UnityEngine.UI;

// public class BlinkRawImage : MonoBehaviour
// {
//     public float blinkSpeed = 1.0f;  // 点滅速度を調整
//     private RawImage rawImage;
//     private Color originalColor;
//     private bool isBlinking = false;

//     void Start()
//     {
//         // RawImageコンポーネントを取得
//         rawImage = GetComponent<RawImage>();

//         // 元の色を保存
//         if (rawImage != null)
//         {
//             originalColor = rawImage.color;
//         }

//         StartBlinking();
//     }

//     void Update()
//     {
//         if (isBlinking)
//         {
//             // アルファ値を時間に応じて変化させる（0から1の間を往復）
//             float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);

//             // アルファ値を適用した色を設定
//             rawImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
//         }
//     }

//     // 点滅を開始する
//     public void StartBlinking()
//     {
//         isBlinking = true;
//     }

//     // 点滅を停止する
//     public void StopBlinking()
//     {
//         isBlinking = false;
//         rawImage.color = originalColor;  // 元の色に戻す
//     }
// }