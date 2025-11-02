using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScr : MonoBehaviour
{
    [SerializeField]
    Image panel;
    float currentAlpha = 0;
    bool isBlacking = false;
    Color panelColor;
    float timer = 0f;
    public static SceneManagerScr Instance;
    private float fadeDuration = 1.0f;
    public static int urasshnumber = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        panel.gameObject.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // panelColor = panel.color;
        // panelColor.a = 0f;
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeAndLoad(string sceneName)
    {
        StartCoroutine(FadeSceneChange(sceneName));
    }

    private IEnumerator FadeSceneChange(string sceneName)
    {
        // フェードアウト
        yield return StartCoroutine(FadeOut());
        // シーン切り替え
        yield return SceneManager.LoadSceneAsync(sceneName);
        // フェードイン
        yield return StartCoroutine(FadeIn());
    }

    // public IEnumerator Fade(float targetAlpha)
    // {
    //     timer = 0;
    //     while (timer <= 2.0f)
    //     {
    //         timer += Time.deltaTime;
    //         float t = timer * 1 / 2;
    //         float a = Mathf.Lerp(currentAlpha, targetAlpha, t);
    //         panelColor.a = a;
    //         panel.color = panelColor;

    //         yield return null;
    //     }
    //     panelColor.a = targetAlpha;
    //     panel.color = panelColor;
    // }

    private IEnumerator FadeOut()
    {
        panel.gameObject.SetActive(true);

        float t = 0;
        Color c = panel.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / fadeDuration);
            panel.color = c;
            yield return null;
        }

        panel.gameObject.SetActive(false);
    }

    private IEnumerator FadeIn()
    {
        panel.gameObject.SetActive(true);

        float t = 0;
        Color c = panel.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1, 0, t / fadeDuration);
            panel.color = c;
            yield return null;
        }

        panel.gameObject.SetActive(false);
    }
}
