using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerClickHandler
{
    public Transform Camera;
    public float walkSpeed; // 歩くスピード
    public float runSpeed; // 走るスピード
    public float RotationSleed;
    public float runThreshold = 50f; // ドラッグ距離の閾値（歩く／走る）

    public RawImage rawImageMouse;  // エディタから割り当てるプレファブ
    public GameObject goImageMouse;  // エディタから割り当てるプレファブ

    Vector3 speed = Vector3.zero;
    Vector3 rot = Vector3.zero;

    public Animator PlayerAnimator;
    bool isWalk;
    bool isRun;

    private Vector2 touchStart;
    private Vector2 touchEnd;

    // JUMPのための変数たち start
    private float jumpHeight = 1.0f;  // ジャンプの高さ
    private float jumpDistance = 1.5f;  // ジャンプの距離（後ろに飛ぶ距離）
    private float jumpDuration = 0.8f;  // ジャンプの時間
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isJumping = false;
    private float jumpStartTime;
    // JUMPのための変数たち end

    [SerializeField] public GameObject prefabToSpawn; // 生成するプレハブ

    private float spawnHeight = 1.5f; // 生成する高さのオフセット


    // Start is called before the first frame update
    void Start()
    {
        Camera.transform.position = transform.position;
    }

    bool isDragging;
    // Update is called once per frame
    void Update()
    {
        if (isJumping)
        {
            float timeSinceStart = Time.time - jumpStartTime;
            float fractionOfJump = timeSinceStart / jumpDuration;

            if (fractionOfJump >= 1)
            {
                isJumping = false;
                transform.position = targetPosition;
                PlayerAnimator.SetBool("isJump", false);
            }
            else
            {
                // 現在のジャンプの進行状況に基づいて位置を補間
                float currentHeight = Mathf.Sin(Mathf.PI * fractionOfJump) * jumpHeight;
                Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, fractionOfJump);
                currentPos.y += currentHeight;
                transform.position = currentPos;
                // カメラを追随させる
                Camera.transform.position = transform.position;
            }
            return;
        }
#if UNITY_EDITOR


        // ドラッグ開始を検出
        if (Input.GetMouseButtonDown(0))
        {

            // UI要素上でクリックされたかどうかをチェック
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            isDragging = true;
            touchStart = Input.mousePosition;
            if (!(touchStart.x > Screen.width / 2))
            {
                PlayerAnimator.SetBool("isWalk", true);
            }
        }

        // ドラッグ中のマウスの移動を追跡
        if (isDragging)
        {
            touchEnd = Input.mousePosition;
            if (touchStart.x > Screen.width / 2)
            {
                RotateCamera(touchStart, touchEnd);
                //return;
            }
            else
            {
                MoveCharacter(touchStart, touchEnd);
            }
        }

        // ドラッグ終了を検出
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            isRun = false;
            PlayerAnimator.SetBool("isRun", false);
            PlayerAnimator.SetBool("isWalk", false);
        }

#else
        if(Input.touchCount < 1)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchStart = touch.position;
                if (!(touchStart.x > Screen.width / 2))
                {
                    PlayerAnimator.SetBool("isWalk", true);
                }
                break;

            case TouchPhase.Moved:
            case TouchPhase.Stationary:
            
                touchEnd = touch.position;
                if(touchStart.x > Screen.width / 2){
                    RotateCamera(touchStart, touchEnd);
                }else{
                    MoveCharacter(touchStart, touchEnd);
                }
                break;
            case TouchPhase.Ended:
                isRun = false;
                PlayerAnimator.SetBool("isRun", false);
                PlayerAnimator.SetBool("isWalk", false);
                break;
        }
#endif


        //PcMove();
        //Rotation();
    }


    public void StartJump()
    {

        PlayerAnimator.SetBool("isJump", true);
        startPosition = transform.position;
        Vector3 backwardDirection = -transform.forward;  // 現在の向いている方向の逆方向
        targetPosition = startPosition + backwardDirection * jumpDistance;
        targetPosition.y = startPosition.y;  // 着地時の高さを同じにする
        isJumping = true;
        jumpStartTime = Time.time;

        Vector3 spawnPosition = gameObject.transform.position + Vector3.up * spawnHeight;
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

    }

    void RotateCamera(Vector2 pStart, Vector2 pEnd)
    {
        Vector2 direction = pEnd - pStart;
        // Mathf.Atan2を使用して方向ベクトルから角度を計算
        // 10で割るのは、速度をゆっくりにするため
        float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg / 10f;


        // 現在のカメラの向きから、ドラッグ分回転した向きを目標とする
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0) * Camera.rotation;
        // 時間をかけて滑らかに回転させる
        Camera.rotation = Quaternion.Lerp(Camera.rotation, targetRotation, Time.deltaTime * 5);

        //Vector2 direction = pEnd - pStart;
        //float yaw = Mathf.Atan2(direction.x, Vector2.zero.magnitude) * Mathf.Rad2Deg / 10f; // 横方向の回転
        //float pitch = Mathf.Atan2(direction.y, Vector2.zero.magnitude) * Mathf.Rad2Deg / 10f; // 縦方向の回転

        //Quaternion targetRotation = Quaternion.Euler(-pitch, yaw, 0) * Camera.rotation;
        //Camera.rotation = Quaternion.Lerp(Camera.rotation, targetRotation, Time.deltaTime * 5);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (Input.GetMouseButtonDown(0))  // 左クリック（またはタッチ）があった場合
        //{
            // マウス位置をワールド座標に変換
            //Vector3 touchPos = eventData.pressEventCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, eventData.pressEventCamera.nearClipPlane));
            //// Y座標を修正（地面や特定の高さに合わせる）
            //touchPos.y = 0;

            //// プレファブからオブジェクトを生成し、タッチ位置に配置
            //Instantiate(goImageMouse, touchPos, Quaternion.identity);
        //}

    }

    private void MoveCharacter(Vector2 pStart, Vector2 pEnd)
    {
        Vector2 direction = pEnd - pStart;
        float dragDistance = Vector3.Distance(pStart, pEnd);
        //Debug.Log(pStart + pEnd + " dragDistance:" + dragDistance);
        goImageMouse.transform.position = pStart; // マウスポインタ表示

        //// ここで移動方向を調整します。
        //// 例えば、スワイプの長さに基づいて速度を変えたい場合、ここで計算します。
        //Vector3 moveDirection = new Vector3(direction.x, 0, direction.y).normalized;
        //// 向きを変える
        //Vector3 rotation = new Vector3(direction.x, 0, direction.y);


        // Mathf.Atan2を使用して方向ベクトルから角度を計算
        float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //Debug.Log(pStart + pEnd + " dragDistance:" + dragDistance + " rotate:" + targetAngle + " Camera" + Camera.eulerAngles.y);
        targetAngle += Camera.eulerAngles.y;
        // 計算した角度でキャラクターの向きを更新
        transform.eulerAngles = new Vector3(0, targetAngle, 0);

        Vector3 walkDirection = Vector3.zero;
        float speed;
        if (dragDistance > runThreshold)
        {
            if (!isRun)
            {
                isRun = true;
                PlayerAnimator.SetBool("isRun", true);

            }
            speed = runSpeed;
        }
        else
        {
            if (isRun)
            {
                isRun = false;
                PlayerAnimator.SetBool("isRun", false);

            }
            speed = walkSpeed;

        }
        // 進める
        walkDirection.z = speed * Time.deltaTime;
        transform.Translate(walkDirection);
        // カメラを追随させる
        Camera.transform.position = transform.position;



        //// キャラクターを移動させる
        //transform.Translate(moveDirection * Time.deltaTime * 2); // 移動速度は適宜調整してください。

        //// キャラクターの向きを変える
        //if (moveDirection != Vector3.zero)
        //{
        //    //Vector2 directionCharctor = 
        //    speed = Vector3.zero;
        //    rot = Vector3.zero;
        //    rot.y = direction.y - transform.eulerAngles.y;
        //    transform.eulerAngles = transform.transform.eulerAngles + rot;
        //    //Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        //    //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360 * Time.deltaTime);


        //}
    }

    private void Move()
    {
        
    }

    private void PcMove()
    {
        speed = Vector3.zero;
        rot = Vector3.zero;
        isWalk = false;

        if (Input.GetKey(KeyCode.W))
        {
            rot.y = 0;
            MoveSet();
        }
        if (Input.GetKey(KeyCode.S))
        {
            rot.y = 180;
            MoveSet();
        }
        if (Input.GetKey(KeyCode.A))
        {
            rot.y = -90;
            MoveSet();
        }
        if (Input.GetKey(KeyCode.D))
        {
            rot.y = 90;
            MoveSet();
        }

        transform.Translate(speed);
        PlayerAnimator.SetBool("isRun", isWalk);
    }

    private void MoveSet()
    {
        speed.z = walkSpeed;
        transform.eulerAngles = Camera.transform.eulerAngles + rot;
        isWalk = true;

    }

    private void Rotation()
    {
        var speed = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speed.y = RotationSleed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            speed.y = -RotationSleed;
        }

        Camera.transform.eulerAngles += speed;
    }
}
