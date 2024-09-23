using UnityEngine;

public class FallingObject : MonoBehaviour
{
    SearchDirector sDirectore;

    void Start()
    {
        sDirectore = GameObject.Find("SearchDirector").GetComponent<SearchDirector>();

    }

    void OnCollisionEnter(Collision collision)
    {
        // キャラクターとの衝突を無視する
        if (collision.gameObject.CompareTag("hero"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            return;
        }

        if (collision.gameObject.CompareTag("Ground")) // 地面のタグが"Ground"の場合
        {
            sDirectore.AnaHoru(gameObject.transform);
            Destroy(gameObject); // オブジェクトを削除
        }
    }

}