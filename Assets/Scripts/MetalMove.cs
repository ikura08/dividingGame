using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class MetalMove : MonoBehaviour, IMovable, IJumpable
{
    PlayerMove playerMoveScr;
    AbilityManager abilityManagerScr;
    [SerializeField]
    bool tap = false;
    [SerializeField]
    bool jumpFragM = false;  //メタルのジャンプ能力
    Rigidbody2D Mrb;
    int jumpCount = 1;
    bool isGrounded;
    float moveSpeed = 5.0f;
    public bool currentCharacterM = false;  //操作対象
    public Transform playerTransform;  //プレイヤーの座標
    public float distance; //このメタルとプレイヤーの距離
    public Renderer myRenderer;
    Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        Mrb = GetComponent<Rigidbody2D>();
        playerMoveScr = FindObjectOfType<PlayerMove>();
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
    }  //Updateの終わり

    public void Tap()
    {
        tap = true;
        playerMoveScr.currentCharacterP = false;
        currentCharacterM = true;
        Debug.Log("実行された");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1;
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Metal"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f) // 上から当たっているか判定
                {
                    jumpCount = 1; // Metalに着地 → ジャンプ回数回復
                    isGrounded = true;
                    break; // 条件を満たしたら1つで十分
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    IEnumerator EnableJumpAfterDelay()  //能力分離と同時にジャンプしないために
    {
        yield return new WaitForSeconds(0.1f);
        jumpFragM = true;
    }

    public void Jump()
    {
        if (jumpCount > 0)
        {
            Mrb.velocity = new Vector2(Mrb.velocity.x, 0); // Y速度リセット
            Mrb.AddForce(new Vector2(0f, 8.0f), ForceMode2D.Impulse);
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
}