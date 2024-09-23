using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // プレイヤーのTransformをInspectorで指定
    private float followDistance = 3.0f; // 歩き始める距離
    private float runDistance = 5.0f; // 走り始める距離
    private float runFastDistance = 10.0f; // 走り始める距離
    private float walkSpeed = 1.0f; // 歩く速度
    private float runSpeed = 2.0f; // 走る速度
    private float runFastSpeed = 3.0f; // 走る速度
    private float rotationSpeed = 5.0f; // 向きを変える速度

    public Animator PlayerAnimator;

    void Update()
    {
        // NPCとプレイヤーの距離を計算
        float distance = Vector3.Distance(transform.position, player.position);

        // 距離に応じて速度を設定
        float currentSpeed = 0.0f;

        if (distance > runFastDistance)
        {
            // 距離が走り始める距離以上の場合、走る
            currentSpeed = runFastSpeed;
            PlayerAnimator.SetBool("isWalk", true);
            PlayerAnimator.SetBool("isRun", true);
            Debug.Log("fast run");
        }
        else if (distance > runDistance)
        {
            // 距離が走り始める距離以上の場合、走る
            currentSpeed = runSpeed;
            PlayerAnimator.SetBool("isWalk", true);
            PlayerAnimator.SetBool("isRun", true);
            Debug.Log("run");
        }
        else if (distance > followDistance)
        {
            // 距離が歩き始める距離以上の場合、歩く
            currentSpeed = walkSpeed;
            PlayerAnimator.SetBool("isRun", false);
            PlayerAnimator.SetBool("isWalk", true);
            Debug.Log("walk");
        }
        else
        {
            PlayerAnimator.SetBool("isWalk", false);
            PlayerAnimator.SetBool("isRun", false);
            Debug.Log("idle");
        }

        // プレイヤーに向かって移動
        if (currentSpeed > 0.0f)
        {
            // 移動
            Vector3 direction = (player.position - transform.position).normalized;

            // 回転を行う
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            transform.position += direction * currentSpeed * Time.deltaTime;
        }
    }
}