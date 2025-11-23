using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement; // ← 移動情報を読む

    // Start
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    // Update
    void Update()
    {
        float x = movement.currentDirection.x;

        if (x > 0)
        {
            animator.SetInteger("State", 1);   // 右
        }
        else if (x < 0)
        {
            animator.SetInteger("State", -1);  // 左
        }
        else
        {
            animator.SetInteger("State", 0);   // 静止
        }
    }
}
