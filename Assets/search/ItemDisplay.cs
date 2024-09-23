using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public RenderTexture renderTexture;
    public Camera itemCamera;
    public Image displayImage;
    public GameObject itemPrefab;

    private GameObject currentItem;

    void Start()
    {
        // カメラのTargetTextureを設定
        itemCamera.targetTexture = renderTexture;

        // UI ImageのソースにRenderTextureを設定
        //displayImage.material.mainTexture = renderTexture;

        // アイテムを表示
        ShowItem(itemPrefab);
    }

    public void ShowItem(GameObject itemToShow)
    {
        // 既存のアイテムを削除
        if (currentItem != null)
            Destroy(currentItem);

        // 新しいアイテムを生成
        //currentItem = Instantiate(itemToShow, itemCamera.transform.position + itemCamera.transform.forward * 2, Quaternion.identity);
        currentItem = Instantiate(itemToShow, itemToShow.transform.position, Quaternion.identity);

        // アイテムをゆっくり回転させる
        currentItem.AddComponent<RotateObject>();
        
        // 元のアイテムを消す
        Destroy(itemPrefab);
    }
}

// アイテムを回転させるシンプルなスクリプト
public class RotateObject : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up, 20 * Time.deltaTime);
    }
}