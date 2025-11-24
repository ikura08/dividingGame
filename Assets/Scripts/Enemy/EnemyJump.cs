using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public float timer = 0f;
    public int jumpCount = 2;
    public float jumpInterval = 4f;
    public float jumpForce = 8f;
    public bool isGround = true;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= jumpInterval && jumpCount == 2)
        {
            timer = 0f;
            jumpCount = 1;
            Jump();
        }

        if (timer >= 0.1f && isGround == true && jumpCount == 1)
        {
            Jump();
            timer = 0f;
            jumpCount = 2;
        }
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            timer = 0f;
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
