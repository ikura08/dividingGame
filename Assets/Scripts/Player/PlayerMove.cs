using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using UnityEditor;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour, IJumpable  //ここでふたつのinterfaceを適用してる
{
    [SerializeField] private CameraMove cameraMove;

    [SerializeField] private GameObject firstArea;
    public float firstY;
    [SerializeField] private GameObject secondArea;
    public float secondY;
    [SerializeField] private GameObject thirdArea;
    public float thirdY;
    public int jumpCount = 1;
    float jumpForce = 7f;
    Rigidbody2D Prb;
    public bool isGrounded;
    float moveSpeed = 4f;
    public bool currentCharacterP = true; //現在の操作対象
    Vector2 velocity;
    float timer;
    Color pC;  //プレイヤーのColor
    SpriteRenderer pR;  //プレイヤーのRenderer
    int phase = 2; //0→ポイズンに当たってから画面が黒くなるまで、1→画面が黒くなってからプレイヤーが移動して画面が明るくなるまで、2→通常
    private Vector2 currentDirection = Vector2.zero;
    private Coroutine moveCoroutine;
    [SerializeField]
    private Image blackPanel; //全体を覆っているパネル
    [SerializeField]
    private Text outText;
    [SerializeField]
    private GameObject abilityManager;
    public AbilityManager abilityManagerScr;
    public PlayerAnimation playerAnimationScr;

    void Start()
    {
        Prb = this.GetComponent<Rigidbody2D>();
        pR = gameObject.GetComponent<SpriteRenderer>();
        pC = Color.white;
        abilityManagerScr = abilityManager.GetComponent<AbilityManager>();
        playerAnimationScr = GetComponent<PlayerAnimation>();
        playerAnimationScr.currentState = 0;
    }

    void Update()
    {
        velocity = Prb.velocity;

        if (gameObject.transform.position.y <= -2.5)
        {
            timer += Time.deltaTime;
            float a = Mathf.Lerp(1, 0, timer * 2);
            pC.a = a;
            pR.color = pC;
            if (timer >= 0.5f)
            {
                transform.position = new Vector3(0, 0, 0);
                pC.a = 1;
                pR.color = pC;
                timer = 0;
            }
        }

        

        // アニメーション
        if (currentDirection.x > 0)
            playerAnimationScr.currentState = 1; //右
        else if (currentDirection.x < 0)
            playerAnimationScr.currentState = -1; //左
        else
            playerAnimationScr.currentState = 0;

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(0, 0, 0);
            Scene currentScene1 = SceneManager.GetActiveScene();
            // SceneManager.LoadScene(currentScene1.name);
            SceneManagerScr.Instance.FadeAndLoad(currentScene1.name);
        }
        if (phase == 0)
        {
            timer += Time.deltaTime;
            Color c = blackPanel.color;
            c.a = Mathf.Lerp(0f, 1f, timer * 4);
            blackPanel.color = c;
            if (timer >= 0.25f)
            {
                timer = 0f;
                transform.position = new Vector2(0f, 0f);
                phase = 1;
                outText.gameObject.SetActive(false);
            }

            Scene currentScene2 = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene2.name);
            // SceneManagerScr.Instance.FadeAndLoad(currentScene2.name);
        }
        else if (phase == 1)
        {
            timer += Time.deltaTime;

            Color c = blackPanel.color;
            c.a = Mathf.Lerp(1f, 0f, timer * 4);
            blackPanel.color = c;
            if (timer >= 0.25f)
            {
                timer = 0f;
                phase = 2;
            }
        }

    }  //Updateの終わり

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
            currentDirection = Vector2.left;
        else if (Input.GetKey(KeyCode.D))
            currentDirection = Vector2.right;
        else
            currentDirection = Vector2.zero;

        // 移動
        transform.position += (Vector3)(currentDirection * moveSpeed * Time.deltaTime);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground に乗ったとき
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 1;
            isGrounded = true;
        }

        // Core に乗ったとき
        if (collision.gameObject.CompareTag("Core"))
        {
            abilityManagerScr.CoreChanging(collision);

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

        if (collision.gameObject.CompareTag("Poison"))
        {
            outText.gameObject.SetActive(true);
            phase = 0;
        }

        // if (collision.gameObject.CompareTag("Goal"))
        // {
        //     spaceText.text = "Goal!";
        //     //うらっしゅここ確認して！！！byけけ  SceneManagerっていう名前のスクリプト作ってました、解決！byいくら withうらっしゅ
        //     //SceneManager.LoadScene("Select");だけだとなんでか'SceneManager' に 'LoadScene' の定義がありませんCS0117って出ちゃう
        //     LoadRandomScene();
        // }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Core"))
        {
            isGrounded = false;
        }
    }

    // interfaceの具体的な動作内容

    public void Jump()
    {
        if (jumpCount >= 1)
        {
            Prb.velocity = new Vector2(Prb.velocity.x, 0); // Y速度リセット
            Prb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount--;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == firstArea)
            cameraMove.SetArea(1, firstY);

        if (other.gameObject == secondArea)
            cameraMove.SetArea(2, secondY);

        if (other.gameObject == thirdArea)
            cameraMove.SetArea(3, thirdY);
    }
}
