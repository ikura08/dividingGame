using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoreWarp : MonoBehaviour, ITrigger, ICollection
{
    public GameObject pairObject;
    public GameObject playerObject;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Rigidbody2D coreRB;
    Vector2 warpVector;
    Vector3 scale;
    Vector3 originalScale;
    bool playerTouch = false;



    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTouch = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTouch = false;
        }
    }

    public void CoreTrigger()
    {
        if (playerTouch == true)
        {
            warpVector = new Vector2(pairObject.transform.position.x, pairObject.transform.position.y + 1.5f);
            playerObject.transform.position = warpVector;
        }
    }

    public void CoreCollection()
    {
        coreRB.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<BoxCollider2D>().isTrigger = true;
        CompositeCollider2D compCol = GetComponent<CompositeCollider2D>();
        if (compCol != null)
            compCol.isTrigger = true;
        StartCoroutine(ShrinkAndDisable());
    }
    private IEnumerator ShrinkAndDisable()
    {
        while (scale.x > 0.01f)
        {
            // サイズを0.2倍ずつ縮小
            scale *= 0.8f;
            transform.localScale = scale;

            transform.position = Vector2.Lerp(transform.position, playerTransform.position, 0.2f);

            yield return new WaitForSeconds(0.04f); // 間隔（速さ調整可）
        }

        // 最後に完全に消して非アクティブ化
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void CoreBring()
    {
        coreRB.bodyType = RigidbodyType2D.Dynamic;
        gameObject.SetActive(true);
        GetComponent<BoxCollider2D>().isTrigger = false;
        CompositeCollider2D compCol = GetComponent<CompositeCollider2D>();
        if (compCol != null)
            compCol.isTrigger = false;
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
