using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public class ImagePulse : MonoBehaviour
{
    private float duration = 1.2f;  // アニメーションの継続時間
    private float maxScale = 1.5f;  // 最大スケール
    private float waitTime = 0.5f;  // アニメーションの繰り返し間の待機時間
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    Coroutine pulseCoroutine;
    public Color startColor = Color.red; // 開始色
    public Color endColor = Color.white;     // 終了色
    public RawImage rawImage;               // 変更対象のRawImageコンポーネント

    void Start()
    {
        Init();
        StartPulseLv2();
    }

    public void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void StartPulseLv1(){
        StartPulse(1.5f);
    }

    public void StartPulseLv2(){
        StartPulse(2f);
    }

    public void StartPulse(float scale){
        if(rectTransform == null){
            Init();
        }
        if(pulseCoroutine != null){
            StopPulseLoop();
        }
        maxScale = scale;
        pulseCoroutine = StartCoroutine(PulseLoop());

    }

    void StopPulseLoop()
    {
        if (pulseCoroutine != null)
        {
            StopCoroutine(pulseCoroutine);
            pulseCoroutine = null;
        }
    }

    IEnumerator PulseLoop()
    {
        Vector3 originalScale = rectTransform.localScale;

        while (true)
        {
            // Pulse処理
            float time = 0;
            while (time < duration)
            {
                float scale = Mathf.Lerp(1.0f, maxScale, time / duration);
                canvasGroup.alpha = 1.0f - (time / duration);
                rectTransform.localScale = originalScale * scale;
                // 色の補間
                rawImage.color = Color.Lerp(startColor, endColor, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 0;
            rectTransform.localScale = originalScale;

            // 色を元に戻す
            rawImage.color = startColor;

            // アニメーション間の待機
            yield return new WaitForSeconds(waitTime);
        }
    }
}