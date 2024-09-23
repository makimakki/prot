using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingoScript : MonoBehaviour
{
    [SerializeField] private Rigidbody[] rigidbodies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGravity()
    {
        foreach(Rigidbody rg in rigidbodies)
        {
            rg.useGravity = true;
        }
    }

    // 衝突が始まった時に呼び出されるメソッド
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "hero") { 
            SetGravity();
        }
        //Debug.Log("ぶつかったオブジェクト: " + collision.gameObject.name);
    }
}
