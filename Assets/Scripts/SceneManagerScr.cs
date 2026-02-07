using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagerScr : MonoBehaviour
{
    [SerializeField]
    Image panel;
    public static SceneManagerScr Instance;
    private float fadeDuration = 1.0f;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text retryText;
    [SerializeField] private float gameOverFadeDuration = 1.1f;
    public bool isGameOver = false;
    public bool isProcessing = false;
    public SoundConfig soundConfig;

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
        if (Input.GetKeyDown(KeyCode.R) && isGameOver && !isProcessing)
        {
            Debug.Log("ゲームオーバーの時のリトライ");
            Instance.Retry();
        }
    }

    public void FadeAndLoad(string sceneName)
    {
        if (isProcessing) return;
        Debug.Log("リトライ中");
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

    private IEnumerator FadeOut()
    {
        panel.gameObject.SetActive(true);

        float t = 0;
        Color c = panel.color;

        if (c.a >= 1f)
            yield break;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / fadeDuration);
            panel.color = c;
            yield return null;
        }

        // panel.gameObject.SetActive(false);
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

    public void GameOver()
    {
        isGameOver = true;
        // BatteryController.Instance.currentBattery = 10;
        StartCoroutine(GameOverSequence());
        StartCoroutine(DelayedGameoverClip(0.5f));
    }

    IEnumerator DelayedGameoverClip(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioSource.PlayClipAtPoint(soundConfig.diePClip, Camera.main.transform.position, 0.7f);
    }

    public IEnumerator GameOverSequence()
    {
        // Retryテキストは非表示
        retryText.gameObject.SetActive(false);
        
        // PanelとGameOverテキストを表示
        panel.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);

        // フェード用カラー
        Color panelColor = panel.color;
        Color gameOverColor = gameOverText.color;

        panelColor.a = 0;
        gameOverColor.a = 0;

        panel.color = panelColor;
        gameOverText.color = gameOverColor;

        float t = 0;
        while (t < gameOverFadeDuration)
        {
            t += Time.deltaTime;
            float lerp = t / gameOverFadeDuration;

            panelColor.a = Mathf.Lerp(0, 1, lerp);
            gameOverColor.a = Mathf.Lerp(0, 1, lerp);

            panel.color = panelColor;
            gameOverText.color = gameOverColor;

            yield return null;
        }

        // フェード完了時、Retryテキストを表示
        retryText.gameObject.SetActive(true);
    }

    public void Retry()
    {
        if (isProcessing) return;
        StartCoroutine(RetrySequence());
    }

    private IEnumerator RetrySequence()
    {
        retryText.gameObject.SetActive(false);

        panel.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);

        Color panelColor = panel.color;
        Color gameOverColor = gameOverText.color;

        float t = 0;

        // 黒フェードイン（黒くなる）
        while (t < gameOverFadeDuration)
        {
            t += Time.deltaTime;
            float lerp = t / gameOverFadeDuration;

            panelColor.a = Mathf.Lerp(panelColor.a, 1f, lerp);
            gameOverColor.a = Mathf.Lerp(gameOverColor.a, 1f, lerp);

            panel.color = panelColor;
            gameOverText.color = gameOverColor;

            yield return null;
        }

        // シーン再読み込み
        Scene current = SceneManager.GetActiveScene();
        yield return SceneManager.LoadSceneAsync(current.name);
        isGameOver = false;

        // Panel と GameOverText フェードアウト（明るく・文字消す）
        t = 0;
        while (t < gameOverFadeDuration)
        {
            t += Time.deltaTime;
            float lerp = t / gameOverFadeDuration;

            panelColor.a = Mathf.Lerp(1f, 0f, lerp);
            gameOverColor.a = Mathf.Lerp(1f, 0f, lerp);

            panel.color = panelColor;
            gameOverText.color = gameOverColor;

            yield return null;
        }

        panel.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }

}
