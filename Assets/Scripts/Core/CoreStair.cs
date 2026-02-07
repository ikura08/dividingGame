using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreStair : MonoBehaviour, ITrigger, ICollection
{
    public CoreProvider provider;
    [SerializeField]
    private Transform playerTransform;
    private Rigidbody2D coreRB;
    private BoxCollider2D coreBox;
    private CompositeCollider2D coreComposite;
    Vector3 scale;
    Vector3 originalScale;
    public int thisCoreNumber;
    public bool isCreating = false;
    public SoundConfig soundConfig;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
        originalScale = transform.localScale;
        coreRB = GetComponent<Rigidbody2D>();
        coreBox = GetComponent<BoxCollider2D>();
        coreComposite = GetComponent<CompositeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CoreTrigger()
    {
        if (isCreating == false)
        {
            provider.point = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -0.5f);
            StartCoroutine(Sequence());

            isCreating = true;
        }
    }

    private IEnumerator Sequence()
    {
        AudioSource.PlayClipAtPoint(soundConfig.provideClip, transform.position, 0.6f);
        StartCoroutine(provider.ProvidingX(originalScale, thisCoreNumber));
        yield return new WaitForSeconds(0.2f);

        AudioSource.PlayClipAtPoint(soundConfig.provideClip, transform.position, 0.6f);
        StartCoroutine(provider.ProvidingY(originalScale, thisCoreNumber));
        yield return new WaitForSeconds(0.2f);

        AudioSource.PlayClipAtPoint(soundConfig.provideClip, transform.position, 0.6f);
        StartCoroutine(provider.ProvidingX(originalScale, thisCoreNumber));
        yield return new WaitForSeconds(0.2f);

        AudioSource.PlayClipAtPoint(soundConfig.provideClip, transform.position, 0.6f);
        StartCoroutine(provider.ProvidingY(originalScale, thisCoreNumber));
        yield return new WaitForSeconds(0.2f);

        AudioSource.PlayClipAtPoint(soundConfig.provideClip, transform.position, 0.6f);
        StartCoroutine(provider.ProvidingX(originalScale, thisCoreNumber));
        yield return new WaitForSeconds(2.0f);

        AudioSource.PlayClipAtPoint(soundConfig.provideClip, transform.position, 0.6f);
        // provider.DestroyAllCores(thisCoreNumber);
        StartCoroutine(provider.Blinking(thisCoreNumber));
        isCreating = false;
    }

    public void CoreCollection()
    {
        coreRB.bodyType = RigidbodyType2D.Kinematic;
        coreBox.isTrigger = true;
        coreComposite.isTrigger = true;
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
        coreRB.bodyType = RigidbodyType2D.Dynamic;
        coreBox.isTrigger = false;
        coreComposite.isTrigger = false;
    }
}
