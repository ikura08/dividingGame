using UnityEngine;

public class PlayerJump : MonoBehaviour, IJumpable
{
    public float jumpForce = 7f;
    public int jumpCount = 1;
    public bool isGrounded = false;

    Rigidbody2D Prb;

    void Start()
    {
        Prb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Jump();
    }

    public void Jump()
    {
        if (jumpCount >= 1)
        {
            Prb.velocity = new Vector2(Prb.velocity.x, 0);
            Prb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount--;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1;
            isGrounded = true;
        }

        // Core は PlayerCoreInteraction 側で処理
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}
