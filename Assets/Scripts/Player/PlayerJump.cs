using UnityEngine;

public class PlayerJump : MonoBehaviour, IJumpable
{
    public float jumpForce = 7f;
    public int jumpCount = 1;
    public bool isGrounded = false;
    Rigidbody2D Prb;
    public SoundConfig soundConfig;

    void Start()
    {
        Prb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            Jump();
    }

    public void Jump()
    {
        if (jumpCount >= 1 && isGrounded == true)
        {
            Prb.velocity = new Vector2(Prb.velocity.x, 0);
            Prb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount--;
            AudioSource.PlayClipAtPoint(soundConfig.jumpPClip, transform.position, 0.7f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Core"))
        {
            jumpCount = 1;
            jumpForce = 7f;
            isGrounded = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Drop"))
        {
            jumpCount = 1;
            jumpForce = 8.5f;
            isGrounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    public void JumpForceRevert()
    {
        jumpForce = 7;
    }

    public void JumpForceUp(float x)
    {
        jumpForce += x;
    }
}
