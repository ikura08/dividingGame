using System.Collections;
using System.Collections.Generic;
// using UnityEditorInternal;
using UnityEngine;

public class CoreJump : MonoBehaviour, ITrigger, ICollection
{
    PlayerMove playerMoveScr;
    AbilityManager abilityManagerScr;
    private Transform playerTransform;
    private Rigidbody2D coreRB;
    Rigidbody2D Mrb;
    int jumpCount = 1;
    bool isGrounded;
    float moveSpeed = 5.0f;
    float jumpForce = 8f;
    public float carryBoost = 1.5f;
    public float distance; //このメタルとプレイヤーの距離
    Vector2 velocity;
    Vector3 scale;
    Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        Mrb = GetComponent<Rigidbody2D>();
        playerMoveScr = FindObjectOfType<PlayerMove>();
        abilityManagerScr = FindObjectOfType<AbilityManager>();

        scale = transform.localScale;
        originalScale = transform.localScale;
        // metalRenderer = this.gameObject.GetComponent<Renderer>();
        // metalRenderer.material = black;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Mrb.velocity;
        // distance = Vector2.Distance(transform.position, playerTransform.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1;
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            jumpCount = 1;
            jumpForce = 25f;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            jumpForce = 8f;
        }
    }

    public void CoreTrigger()
    {
        if (isGrounded == true)
        {
            jumpCount = 1;
            if (jumpCount > 0)
            {
                Mrb.velocity = new Vector2(Mrb.velocity.x, 0); // Y速度リセット
                Mrb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                jumpCount--;
            }
        }
    }
    // public IEnumerator Move(Vector2 direction)
    // {
    //     while (true)
    //     {
    //         transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    //         yield return null;
    //     }
    // }

    public void CoreCollection()
    {
        coreRB.bodyType = RigidbodyType2D.Kinematic;
        StartCoroutine(ShrinkAndDisable());
    }
    private IEnumerator ShrinkAndDisable()
    {
        Vector3 scale = transform.localScale;

        while (scale.x > 0.01f)
        {
            // サイズを0.2倍ずつ縮小
            scale *= 0.8f;
            transform.localScale = scale;

            transform.position = Vector2.Lerp(transform.position, playerTransform.position, 0.2f);

            yield return new WaitForSeconds(0.05f); // 間隔（速さ調整可）
        }

        // 最後に完全に消して非アクティブ化
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void CoreBring()
    {
        coreRB.bodyType = RigidbodyType2D.Dynamic;
        gameObject.SetActive(true);
        StartCoroutine(GrowAndEnable());
    }

    private IEnumerator GrowAndEnable()
    {
        while (scale.x < originalScale.x - 0.01f)
        {
            scale *= 1.2f;
            transform.localScale = scale;

            transform.position = Vector2.Lerp(playerTransform.position, new Vector2(playerTransform.position.x + 1.5f, playerTransform.position.y), 0.2f);

            yield return new WaitForSeconds(0.04f);
        }

        transform.localScale = originalScale;
    }
}