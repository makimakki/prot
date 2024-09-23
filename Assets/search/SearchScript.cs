using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchScript : MonoBehaviour
{
    [SerializeField] private RawImage enImage;
    [SerializeField] private RawImage searchImage;

    private float duration = 1.2f;  // アニメーションの継続時間
    private float maxScale = 1.5f;  // 最大スケール
    private float waitTime = 0.5f;  // アニメーションの繰り返し間の待機時間
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    Coroutine pulseCoroutine;
    private Color startColor = Color.red; // 開始色
    private Color endColor = Color.white;     // 終了色
    
    void Start()
    {
        Init();
        StartPulseLv(1);
    }

    void Update()
    {
        
    }

    public void Init()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void StartPulseLv(int lv){
        float scale = 0;
        if(lv == 1){
            startColor = Color.yellow;
            scale = 1.2f;
        }
        if(lv == 2){
            startColor = new Color(1f, 0.5f, 0f, 1f); 
            scale = 1.4f;
        }
        if(lv == 3){
            startColor = new Color(1f, 0.5f, 0f, 1f); 
            scale = 1.6f;
        }
        if(lv == 4){
            startColor = new Color(0f, 0f, 1f, 1f); 
            scale = 1.8f;
        }
        if(lv == 5){
            startColor = new Color(0.5f, 0f, 1f, 1f); 
            scale = 2.0f;
        }
        StartPulse(scale);
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
                enImage.color = Color.Lerp(startColor, endColor, time / duration);
                searchImage.color = Color.Lerp(startColor, endColor, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 0;
            rectTransform.localScale = originalScale;

            // 色を元に戻す
            enImage.color = startColor;
            //searchImage.color = startColor;

            // アニメーション間の待機
            yield return new WaitForSeconds(waitTime);
        }
    }
}
