using System;
using System.Collections;
using UnityEngine;

public class TakarabakoObject : MonoBehaviour
{
    public GameObject[] items; // 飛び出すアイテムのプレハブ
    private float launchHeight = 3f; // アイテムが飛び上がる高さ
    private float launchDuration = 1f; // アイテムが飛び上がる時間
    private float radius = 2f; // アイテムが飛び出す距離
    private bool hasOpened = false; // 一度だけ開くためのフラグ
    void Start()
    {
        // Resourcesフォルダからプレハブをロード
        items = new GameObject[3];
        items[0] = Resources.Load<GameObject>("item_cake");
        items[1] = Resources.Load<GameObject>("item_pan");
        items[2] = Resources.Load<GameObject>("item_koppu");
    }

    // 宝箱を開ける時に呼ばれるメソッド
    public void OpenChest()
    {
        if (hasOpened) return; // 既に開かれていたら何もしない
        hasOpened = true;

        for (int i = 0; i < items.Length; i++)
        {
            LaunchItem(i);
        }
    }

    private void LaunchItem(int index)
    {
        if (index >= items.Length)
        {
            Debug.LogWarning("アイテムの数が不足しています。");
            return;
        }

        // スポーン地点を計算
        float angle = 360f / items.Length * index;
        Vector3 targetPosition = CalculateSpawnPosition(angle);

        // アイテムをスポーン
        GameObject item = Instantiate(items[index], transform.position, Quaternion.identity);
        item.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        if (index == 0)
        {
            item.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        }

        // 放物線運動をシミュレート
        StartCoroutine(MoveItem(item, targetPosition));
    }

    private Vector3 CalculateSpawnPosition(float angle)
    {
        float radians = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians) * radius;
        float z = Mathf.Sin(radians) * radius;
        return new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
    }

    private IEnumerator MoveItem(GameObject item, Vector3 targetPosition)
    {
        Vector3 startPosition = item.transform.position;
        Vector3 controlPoint = (startPosition + targetPosition) / 2 + Vector3.up * launchHeight;

        float elapsedTime = 0f;

        while (elapsedTime < launchDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / launchDuration;
            item.transform.position = CalculateBezierPoint(t, startPosition, controlPoint, targetPosition);
            yield return null;
        }

        item.transform.position = targetPosition;
        PlaceOnGround(item);
    }

    private void PlaceOnGround(GameObject item)
    {
        RaycastHit hit;
        if (Physics.Raycast(item.transform.position + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity))
        {
            item.transform.position = new Vector3(item.transform.position.x, hit.point.y, item.transform.position.z);
        }
        else
        {
            Debug.LogWarning("地面の位置を検出できませんでした。");
        }
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // u^2 * P0
        p += 2 * u * t * p1; // 2 * u * t * P1
        p += tt * p2; // t^2 * P2

        return p;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトが"Ground"タグを持つ場合に開く
        if (collision.gameObject.CompareTag("Ground"))
        {
            OpenChest();
        }
    }


    //public GameObject[] items; // 飛び出すアイテムのプレハブ
    //private float launchForce = 2f; // アイテムが飛び出す力
    //private float upwardForce = 6f; // アイテムが上に飛び出す力
    //private float radius = 0.3f; // アイテムが飛び出す距離
    //private bool hasOpened = false; // 一度だけ開くためのフラグ



    //// 宝箱を開ける時に呼ばれるメソッド
    //public void OpenChest()
    //{
    //    if (hasOpened) return; // 既に開かれていたら何もしない
    //    hasOpened = true;

    //    for (int i = 0; i < items.Length; i++)
    //    {
    //        LaunchItem(i);
    //    }
    //}

    //private void LaunchItem(int index)
    //{
    //    if (index >= items.Length)
    //    {
    //        Debug.LogWarning("アイテムの数が不足しています。");
    //        return;
    //    }

    //    // スポーン地点を計算
    //    float angle = 360f / items.Length * index;
    //    Vector3 spawnPosition = CalculateSpawnPosition(angle);

    //    // アイテムをスポーン
    //    GameObject item = Instantiate(items[index], spawnPosition, Quaternion.identity);
    //    item.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
    //    if (index == 1)
    //    {
    //        item.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
    //    }

    //    // Rigidbodyを取得または追加
    //    Rigidbody rb = null;
    //    try
    //    {
    //        rb = item.GetComponent<Rigidbody>();
    //    }
    //    catch (Exception e)
    //    {
    //        if (rb == null)
    //        {
    //            rb = item.AddComponent<Rigidbody>();
    //        }
    //    }

    //    // 放物線を描く力を与える
    //    Vector3 forceDirection = (spawnPosition - transform.position).normalized;
    //    Vector3 force = forceDirection * launchForce + Vector3.up * upwardForce;
    //    rb.AddForce(force, ForceMode.Impulse);
    //}

    //private Vector3 CalculateSpawnPosition(float angle)
    //{
    //    float radians = angle * Mathf.Deg2Rad;
    //    float x = Mathf.Cos(radians) * radius;
    //    float z = Mathf.Sin(radians) * radius;
    //    return new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    // 衝突したオブジェクトが"Ground"タグを持つ場合に開く
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        OpenChest();
    //    }
    //}

    //void ScaleObject(GameObject obj, float size)
    //{
    //    Bounds bounds = CalculateBounds(obj);
    //    float maxOriginalDimension = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);

    //    // 新しいスケールを計算
    //    float scaleFactor = size / maxOriginalDimension;
    //    obj.transform.localScale = obj.transform.localScale * scaleFactor;
    //}

    //Bounds CalculateBounds(GameObject obj)
    //{
    //    Bounds bounds = new Bounds(obj.transform.position, Vector3.zero);
    //    Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
    //    foreach (Renderer renderer in renderers)
    //    {
    //        bounds.Encapsulate(renderer.bounds);
    //    }
    //    return bounds;
    //}

}

    //// フェードアウトさせる
    //public float delay = 2.0f;  // フェードアウトを始めるまでの遅延時間
    //public float fadeDuration = 3.0f;  // フェードアウトにかかる時間
    //private Renderer objectRenderer;
    //private Color originalColor;


    //void Start()
    //{
    //    //objectRenderer = GetComponent<Renderer>();
    //    //if (objectRenderer != null)
    //    //{
    //    //    originalColor = objectRenderer.material.color;
    //    //    StartCoroutine(FadeOutCoroutine());
    //    //}
    //}

    //private IEnumerator FadeOutCoroutine()
    //{
    //    yield return new WaitForSeconds(delay);

    //    float elapsedTime = 0f;

    //    while (elapsedTime < fadeDuration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
    //        Color newColor = originalColor;
    //        newColor.a = alpha;
    //        objectRenderer.material.color = newColor;

    //        yield return null;
    //    }

    //    gameObject.SetActive(false);  // フェードアウト後にGameObjectを非アクティブにする
    //}
//}