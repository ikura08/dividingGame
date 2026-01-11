using System.Collections;
using System.Collections.Generic;
// using UnityEditorInternal;
using UnityEngine;

public class CoreMove : MonoBehaviour, IMovable, ITrigger
{
    PlayerMovement playerMoveScr;
    AbilityManager abilityManagerScr;
    [SerializeField]
    bool tap = false;
    [SerializeField]
    bool jumpFragM = false;  //メタルのジャンプ能力
    Rigidbody2D Mrb;
    int jumpCount = 1;
    bool isGrounded;
    float moveSpeed = 5.0f;
    float jumpForce = 8f;
    public bool currentCharacterM = false;  //操作対象
    public Transform playerTransform;  //プレイヤーの座標
    public float distance; //このメタルとプレイヤーの距離
    public Renderer myRenderer;
    Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        Mrb = GetComponent<Rigidbody2D>();
        playerMoveScr = FindObjectOfType<PlayerMovement>();
        abilityManagerScr = FindObjectOfType<AbilityManager>();
        // metalRenderer = this.gameObject.GetComponent<Renderer>();
        // metalRenderer.material = black;
        myRenderer = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Mrb.velocity;
        distance = Vector2.Distance(transform.position, playerTransform.position);
    }

    public void Tap()
    {
        tap = true;
        // playerMoveScr.currentDirection = false;
        currentCharacterM = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("地面に触れている");
            jumpCount = 1;
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Core"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f) // 上から当たっているか判定
                {
                    jumpCount = 1; // Coreに着地 → ジャンプ回数回復
                    isGrounded = true;
                    break; // 条件を満たしたら1つで十分
                }
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("プレイヤーに触れている");
            jumpCount = 1;
            jumpForce = 20f;
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

    IEnumerator EnableJumpAfterDelay()  //能力分離と同時にジャンプしないために
    {
        yield return new WaitForSeconds(0.1f);
        jumpFragM = true;
    }

    public void CoreTrigger()
    {
        if (jumpCount > 0)
        {
            Mrb.velocity = new Vector2(Mrb.velocity.x, 0); // Y速度リセット
            Mrb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount--;
        }
    }
    public IEnumerator Move(Vector2 direction)
    {
        while (true)
        {
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void CoreCollection()
    {
        throw new System.NotImplementedException();
    }

    public void CoreBring()
    {
        throw new System.NotImplementedException();
    }
}