using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public GameObject character;  // キャラクターのオブジェクト
    public RectTransform bubbleRect;  // 吹き出しのRectTransform
    public Vector3 offset = new Vector3(0, 2, 0);  // キャラクターからのオフセット（Y軸を調整）

    void Update()
    {
        // キャラクターのワールド座標をスクリーン座標に変換
        Vector3 screenPos = Camera.main.WorldToScreenPoint(character.transform.position + offset);

        // 吹き出しの位置をスクリーン座標に対応させる
        bubbleRect.position = screenPos;
    }
}