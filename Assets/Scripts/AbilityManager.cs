using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum AbilityType
{
    Jump,
    Move
}

public class AbilityManager : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject selectObject;
    IMovable movable;
    IJumpable jumpable;
    Renderer selectRenderer;
    Rigidbody2D selectRb;
    public AbilityType currentAbility = AbilityType.Jump;
    IconChange iconChangeScr;
    PlayerMove playerMoveScr;
    private Coroutine moveCoroutine;
    private Vector2 currentDirection = Vector2.zero;
    IMovable selectMovable;  //タップしたオブジェクトのmovable
    float yKeyDownTime = -1f;  //回収のためのYボタンを押す時間
    bool hasExecuted = false;  //能力回収をしたかどうか


    // Start is called before the first frame update
    void Start()
    {
        movable = selectObject.GetComponent<IMovable>();
        jumpable = selectObject.GetComponent<IJumpable>();
        selectRenderer = selectObject.GetComponent<Renderer>();
        iconChangeScr = FindObjectOfType<IconChange>();

        playerMoveScr = FindObjectOfType<PlayerMove>();
        selectMovable = playerObject.GetComponent<IMovable>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newDirection = Vector2.zero;
        // ボタン上で何かのオブジェクトをタップしたら、movableのオブジェクトを変える
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);


            if (hit == null)
                return;

            //  オブジェクト交換の操作
            IJumpable hittedJumpable = null;
            IMovable hittedMovable = null;
            if (currentAbility == AbilityType.Jump)
            {
                hittedJumpable = hit.gameObject.GetComponent<IJumpable>();
            }
            if (currentAbility == AbilityType.Move)
            {
                hittedMovable = hit.gameObject.GetComponent<IMovable>();
            }

            if (hittedMovable == null && hittedJumpable == null)
                return;

            if (selectObject != null)
            {
                if (selectRb)
                {
                    Debug.Log("セレクトオブジェクトを変えた");
                    selectRb.velocity = Vector2.zero;
                    selectRb.angularVelocity = 0f;
                    selectRb.bodyType = RigidbodyType2D.Kinematic;
                }
            }

            selectObject = hit.gameObject;
            selectRb = selectObject.GetComponent<Rigidbody2D>();
            // if (selectObject.CompareTag("Metal"))
            // selectRenderer.material = white;

            if (selectRb)
            {
                Debug.Log("重力を戻す");
                selectRb.bodyType = RigidbodyType2D.Dynamic;
                selectRb.gravityScale = 1f;
            }

            if (currentAbility == AbilityType.Jump)
            {
                Debug.Log("ジャンプが変わった");
                jumpable = hittedJumpable;
            }
            if (currentAbility == AbilityType.Move)
            {
                Debug.Log("移動が変わった");
                movable = hittedMovable;
                selectMovable = selectObject.GetComponent<IMovable>();
            }

        }


        //以下、ボタンごとの動作内容
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeAbility();
            iconChangeScr.IconChanging();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpable.Jump();
        }

        if (Input.GetKey(KeyCode.A)) newDirection = Vector2.left;
        else if (Input.GetKey(KeyCode.D)) newDirection = Vector2.right;

        // 入力が変わった or 止める必要があるとき
        if (newDirection != currentDirection)
        {
            // 前のコルーチン停止
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }

            currentDirection = newDirection;

            // 新しい方向があるなら開始
            if (currentDirection != Vector2.zero)
            {
                moveCoroutine = StartCoroutine(selectMovable.Move(currentDirection));
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            yKeyDownTime = Time.time;
            hasExecuted = false;
        }

        // Yキーを押し続けている間の処理
        if (Input.GetKey(KeyCode.Y))
        {
            if (!hasExecuted && Time.time - yKeyDownTime >= 0.7f)
            {
                // 1秒以上経過していたら実行
                movable = playerObject.GetComponent<IMovable>();
                jumpable = playerObject.GetComponent<IJumpable>();
                hasExecuted = true;
            }
        }

        // Yキーを離したらリセット
        if (Input.GetKeyUp(KeyCode.Y))
        {
            yKeyDownTime = -1f;
            hasExecuted = false;
        }


    }

    void ChangeAbility()
    {
        if (currentAbility == AbilityType.Jump)
        {
            currentAbility = AbilityType.Move;
        }
        else
        {
            currentAbility = AbilityType.Jump;
        }
    }

    void AbilityReturn()
    {

    }
}
