using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// using TMPro;

public class TutrialScr : MonoBehaviour
{
    [SerializeField]
    private GameObject firstCore;
    [SerializeField]
    private GameObject secondCore;
    [SerializeField]
    private GameObject thirdCore;
    [SerializeField]
    private TMP_Text spaceText;

    private Color spaceColor;
    public int spaceChecker = 0; 

    private Coroutine fadeCoroutine; // 今動いてるフェードを管理

    void Start()
    {
        spaceColor = spaceText.color;
        spaceColor.a = 0f; // 最初は透明
        spaceText.color = spaceColor;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == firstCore || collision.gameObject == secondCore || collision.gameObject ==  thirdCore)
        {
            spaceText.text = "Space";
            StartFade(1f, 1f); // 1秒かけて不透明に
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == firstCore || collision.gameObject == secondCore || collision.gameObject == thirdCore)
        {
            StartFade(0f, 0.5f); // 0.5秒かけて透明に
        }
    }

    private void StartFade(float targetAlpha, float duration)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (this != null)
        {
            fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha, duration));
        }
    }

    private IEnumerator FadeRoutine(float targetAlpha, float duration)
    {
        float startAlpha = spaceText.color.a;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            spaceColor.a = newAlpha;
            spaceText.color = spaceColor;

            yield return null;
        }

        // 最後に完全に合わせる
        spaceColor.a = targetAlpha;
        spaceText.color = spaceColor;
        fadeCoroutine = null;
    }
}
