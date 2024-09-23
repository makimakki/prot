using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour
{
    public Button buttonA;
    public Button buttonB;
    public Button[] buttons;

    private CanvasGroup canvasGroupB;
    private CanvasGroup[] canvasGroups;
    private Coroutine showButtonBCoroutine;

    void Start()
    {
        canvasGroups = new CanvasGroup[buttons.Length];
        int i = 0;
        foreach (Button b in buttons)
        {
            canvasGroups[i] = b.GetComponent<CanvasGroup>();
            if (canvasGroups[i] == null)
            {
                canvasGroups[i] = b.gameObject.AddComponent<CanvasGroup>();
            }
            canvasGroups[i++].alpha = 0f; // 初期状態でボタンを非表示にする

        }
        //canvasGroupB = buttonB.GetComponent<CanvasGroup>();
        //if (canvasGroupB == null)
        //{
        //    canvasGroupB = buttonB.gameObject.AddComponent<CanvasGroup>();
        //}
        //canvasGroupB.alpha = 0f; // 初期状態でBボタンを非表示にする

        // EventTriggerを設定
        EventTrigger triggerA = buttonA.gameObject.AddComponent<EventTrigger>();

        // ButtonAのPointerDownイベントを設定
        EventTrigger.Entry pointerDownEntryA = new EventTrigger.Entry();
        pointerDownEntryA.eventID = EventTriggerType.PointerDown;
        pointerDownEntryA.callback.AddListener((data) => { OnPointerDownA((PointerEventData)data); });
        triggerA.triggers.Add(pointerDownEntryA);

        // ButtonAのPointerUpイベントを設定
        EventTrigger.Entry pointerUpEntryA = new EventTrigger.Entry();
        pointerUpEntryA.eventID = EventTriggerType.PointerUp;
        pointerUpEntryA.callback.AddListener((data) => { OnPointerUpA((PointerEventData)data); });
        triggerA.triggers.Add(pointerUpEntryA);
    }

    public void OnPointerDownA(PointerEventData data)
    {
        if (showButtonBCoroutine != null)
        {
            StopCoroutine(showButtonBCoroutine);
        }
        //canvasGroupB.alpha = 1f; // Bボタンを表示
        foreach(CanvasGroup cg in canvasGroups)
        {
            cg.alpha = 1f;
        }
        foreach(Button b in buttons)
        {
            b.gameObject.SetActive(true);
        }
    }

    public void OnPointerUpA(PointerEventData data)
    {
        if (showButtonBCoroutine != null)
        {
            StopCoroutine(showButtonBCoroutine);
        }
        Debug.Log("Button A released. Button B will fade out over 0.4 seconds.");

        // PointerEventDataを使用してマウスの位置がButtonBの上か確認
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == buttonB.gameObject)
            {
                StartCoroutine(ClickButtonBEffect());
                showButtonBCoroutine = StartCoroutine(FadeOutButtonB(0.4f, true));
                buttonB.onClick.Invoke(); // ButtonBのクリックイベントを呼び出す
                Debug.Log("Button B clicked.");
                return;
            }
        }
        showButtonBCoroutine = StartCoroutine(FadeOutButtonB(0.4f, false)); // フェードアウト時間を0.4秒に設定
    }

    private IEnumerator ClickButtonBEffect()
    {
        // 点滅エフェクトの実装
        float flashDuration = 0.1f; // 点滅の時間
        Color originalColor = buttonB.image.color;
        Color flashColor = Color.gray; // 点滅色

        // 点滅を二回行う
        for (int i = 0; i < 1; i++)
        {
            buttonB.image.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            buttonB.image.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }

    private IEnumerator FadeOutButtonB(float duration)
    {
        return FadeOutButtonB(duration, false);
    }

    private IEnumerator FadeOutButtonB(float duration, bool isImmediately)
    {
        // すぐに
        if (!isImmediately)
        {
            yield return new WaitForSeconds(2.0f);
        }

        float startAlpha = canvasGroups[0].alpha;
        float rate = 1.0f / duration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            progress += rate * Time.deltaTime;
            foreach(CanvasGroup gp in canvasGroups)
            {
                gp.alpha = Mathf.Lerp(startAlpha, 0, progress);
            }
            //canvasGroupB.alpha = Mathf.Lerp(startAlpha, 0, progress);
            yield return null;
        }
        foreach (CanvasGroup gp in canvasGroups)
        {
            gp.alpha = 0f;
        }
        //canvasGroupB.alpha = 0f;

        foreach (Button b in buttons)
        {
            b.gameObject.SetActive(false);
        }
        Debug.Log("Button B is now inactive.");
    }
}