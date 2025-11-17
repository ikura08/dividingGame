using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBridge : MonoBehaviour, ITrigger, ICollection
{
    public CoreProvider provider;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Rigidbody2D coreRB;
    Vector3 scale;
    Vector3 originalScale;
    public int thisCoreNumber;
    public bool isCreating = false; //スペース連打のバグ修正
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

    public void CoreTrigger()
    {
        if (isCreating == false)
        {
            provider.point = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            StartCoroutine(Sequence());

            isCreating = true;
        }
    }

    private IEnumerator Sequence()
    {
        StartCoroutine(provider.ProvidingX(originalScale, thisCoreNumber));

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(provider.ProvidingX(originalScale, thisCoreNumber));
        }

        yield return new WaitForSeconds(2.0f);
        // provider.DestroyAllCores(thisCoreNumber);
        StartCoroutine(provider.Blinking(thisCoreNumber));

        isCreating = false;
    }

    
    
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
