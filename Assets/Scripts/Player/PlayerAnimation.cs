using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement; // ← 移動情報を読む

    public AbilityManager abilityManager;
    private int preDirection = 1;

    // Start
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        animator.SetInteger("State", 0);
    }

    // Update
    void Update()
    {
        float x = movement.currentDirection.x;

        if (x > 0)
        {
            animator.SetInteger("State", 1);   // 右
            preDirection = 1;
        }
        else if (x < 0)
        {
            animator.SetInteger("State", -1);  // 左
            preDirection = -1;
        }
        else
        {
            animator.SetInteger("State", 0);   // 静止
        }

        if (abilityManager.spaceDuration > 0)
        {
            if (preDirection == 1){
                animator.SetInteger("State", 2);
            }
            else if (preDirection == -1)
            {
                animator.SetInteger("State", -2);
            }
        }
        else if (abilityManager.spaceDuration == 0)
        {
            if (x > 0) animator.SetInteger("State", 1);
            else if (x < 0) animator.SetInteger("State", -1);
            else animator.SetInteger("State", 0);
        }
    }
}
