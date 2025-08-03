using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class PlayerMove : MonoBehaviour, IMovable, IJumpable  //ここでふたつのinterfaceを適用してる
{
    public int jumpCount = 1;
    Rigidbody2D Prb;
    public bool isGrounded;
    float moveSpeed = 5.0f;
    [SerializeField]
    public bool jumpFragP = true; //プレイヤーのジャンプ能力
    [SerializeField]
    public bool moveFragP = true; //プレイヤーの移動能力
    public bool currentCharacterP = true; //現在の操作対象
    Vector2 velocity;
    float timer;
    Color pC;  //プレイヤーのColor
    SpriteRenderer pR;  //プレイヤーのRenderer

    void Start()
    {
        Prb = this.GetComponent<Rigidbody2D>();
        pR = gameObject.GetComponent<SpriteRenderer>();
        pR.color = Color.blue;
    }

    void Update()
    {
        velocity = Prb.velocity;
        if (gameObject.transform.position.y <= -3)
        {
            timer += Time.deltaTime;
            float a = Mathf.Lerp(1, 0, timer);
            pC.a = a;
            pR.color = pC;
        }
    
    }  //Updateの終わり

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground に乗ったとき
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1;
            isGrounded = true;
        }

        // Metal に乗ったとき
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
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Metal"))
        {
            isGrounded = false;
        }
    }

    public void JumpFragRecall()
    {
        jumpFragP = true;
    }

    // interfaceの具体的な動作内容
    public void MoveFragRecall()
    {
        moveFragP = true;
    }

    public void Jump()
    {
        Prb.velocity = new Vector2(Prb.velocity.x, 0); // Y速度リセット
        Prb.AddForce(new Vector2(0f, 10f), ForceMode2D.Impulse);
        jumpCount--;
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
