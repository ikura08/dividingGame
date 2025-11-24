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
    public GameObject coreObject;
    IMovable movable;
    IJumpable jumpable;
    ITrigger trigger;
    ICollection collection;
    Renderer selectRenderer;
    Rigidbody2D selectRb;
    IconChange iconChangeScr;
    PlayerMovement playerMoveScr;
    IMovable selectMovable;  //タップしたオブジェクトのmovable
    bool coreCollection = false;  //メタルを回収をしたかどうか
    [SerializeField]
    private GameObject WholeLight;
    float holdTime = 0f;   // 長押ししている時間
    GameObject target;     // 長押し中のオブジェクト
    private float spaceDuration = 0f;
    private int coreBatteryCost = 20;

    // Start is called before the first frame update
    void Start()
    {
        movable = selectObject.GetComponent<IMovable>();
        jumpable = selectObject.GetComponent<IJumpable>();
        trigger = coreObject.GetComponent<ITrigger>();
        selectRenderer = selectObject.GetComponent<Renderer>();
        iconChangeScr = FindObjectOfType<IconChange>();

        playerMoveScr = FindObjectOfType<PlayerMovement>();
        selectMovable = playerObject.GetComponent<IMovable>();

        // WholeLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            spaceDuration = 0;

        if (Input.GetKey(KeyCode.Space))
            spaceDuration += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.Space))
            spaceDuration = 0;

        if (spaceDuration >= 0.5f)
        {
            if (trigger != null)
            {
                trigger.CoreTrigger();
                BatteryController.Instance.UseBattery(coreBatteryCost);
            }
            spaceDuration = -1.0f;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("ジャンプした");
            // jumpable.Jump();
        }
        //コア回収時のマウス長押し
        if (Input.GetMouseButton(0))
        {
            // ① ScreenToWorldPoint で Z をカメラ距離に合わせる
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)
            );

            // ② XY だけを取り出して 2D に変換
            Vector2 pos = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            // ③ OverlapPoint で判定
            Collider2D hit = Physics2D.OverlapPoint(pos);

            // ④ デバッグ用ログ（必要なら）
            if (hit == null)
                Debug.Log("何も当たらない");
            else
                Debug.Log("当たったオブジェクト: " + hit.name + " (Z=" + hit.transform.position.z + ")");

            if (hit != null && hit.CompareTag("Core"))
            {

                Debug.Log("coreを長押しした！1");
                if (target == hit.gameObject)
                {
                    Debug.Log("coreを長押しした！2");
                    holdTime += Time.deltaTime;
                    if (holdTime >= 1f)
                    {
                        collection = hit.GetComponent<ICollection>();
                        collection.CoreCollection();
                        Debug.Log("coreを長押しした！");
                        // ここに実行したい処理を書く
                        holdTime = 0f; // 一度実行したらリセット
                    }
                }
                else
                {
                    target = hit.gameObject;
                    holdTime = 0f;
                }
            }
            else
            {
                target = null;
                holdTime = 0f;
            }
        }
        else
        {
            target = null;
            holdTime = 0f;
        }

        // if (Input.GetKeyDown(KeyCode.Return))
        // {
        //     collection.CoreBring();
        // }

    } //Updateの終わり

    public void CoreChanging(Collision2D collision)
    {
        Collider2D hit = collision.collider;
        
        ITrigger hittedTrigger = null;
        hittedTrigger = hit.gameObject.GetComponent<ITrigger>();


        if (hittedTrigger == null)
            return;

        if (selectObject != null)
        {
            if (selectRb)
            {
                selectRb.velocity = Vector2.zero;
                selectRb.angularVelocity = 0f;
            }
        }

        selectObject = hit.gameObject;
        selectRb = selectObject.GetComponent<Rigidbody2D>();

        // StartCoroutine(LightAppearance());
        // if (selectObject.CompareTag("core"))
        // selectRenderer.material = white;

        if (selectRb)
        {
            // selectRb.bodyType = RigidbodyType2D.Dynamic;
            selectRb.gravityScale = 1f;
        }

        trigger = hittedTrigger;
    }

    private IEnumerator LightAppearance()
    {
        Vector3 pos = selectObject.transform.position;
        pos.z -= 9f;
        WholeLight.transform.position = pos;

        WholeLight.SetActive(true);

        yield return new WaitForSeconds(0.15f);

        WholeLight.SetActive(false);
        yield return null;
    }
}
