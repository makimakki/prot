using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PopupAnimation : MonoBehaviour
{
    public CanvasGroup canvasGroup;  // UI要素のCanvasGroup
    public RectTransform rectTransform;  // UI要素のRectTransform

    void Start()
    {
        // 初期状態を設定
        canvasGroup.alpha = 0;  // 完全に透明
        rectTransform.localScale = Vector3.zero;  // サイズを0に設定

        // アニメーションを設定
        ShowPopup();
    }

    public void ShowPopup()
    {
        // フェードインしながらスケールアップ
        canvasGroup.DOFade(1, 0.5f);  // 0.5秒でフェードイン
        rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);  // 0.5秒でスケールアップ、イーズアウトを設定
    }
}