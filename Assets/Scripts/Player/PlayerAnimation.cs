using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    public int currentState; // 0→静止、1→右移動、-1→左移動
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == -1)
            animator.SetInteger("State", -1);
        else if (currentState == 0)
            animator.SetInteger("State", 0);
        else if (currentState == 1)
            animator.SetInteger("State", 1);
    }
}
