using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;
using Unity.VisualScripting;
// using UnityEditor.SceneManagement;

public class GoalManager : MonoBehaviour
{
    public static GoalManager Instance;
    public bool isCleared = false;
    public TMP_Text clearText;
    public TMP_Text clickText;
    float fadeSpeed = 1f;
    bool canTap = false;
    public GameObject clearEffectPrefab;
    public Image star;
    public Image flame;
    public int stageNumber;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        star.enabled = false;
        flame.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canTap == true && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1.0f;
            Scene current = SceneManager.GetActiveScene();
            SceneManagerScr.Instance.FadeAndLoad("Select");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isCleared && !SceneManagerScr.Instance.isGameOver)
        {
            Time.timeScale = 0f;
            GameObject effect = Instantiate(clearEffectPrefab, transform.position, Quaternion.identity);
            isCleared = true;
            GetComponent<Renderer>().enabled = false;
            StartCoroutine(FadeInText());
            if (SelectStage.nextStage <= stageNumber)
            {
                SelectStage.nextStage += 1;
            }
        }
    }
    
    private IEnumerator FadeInText()
    {
        Color c = clearText.color;
        float a = 0.0f;
        while (a < 1.0f)
        {
            a += Time.unscaledDeltaTime * fadeSpeed;
            c.a = a;
            clearText.color = c;
            yield return null;
        }

        bool hasCoin = false;
        if (stageNumber > 0)
        {
            hasCoin = CoinData.isStageCoinGet[stageNumber - 1];
            // switch (stageNumber)
            // {
            //     case 1: hasCoin = CoinData.isStage1CoinGet; break;
            //     case 2: hasCoin = CoinData.isStage2CoinGet; break;
            //     case 3: hasCoin = CoinData.isStage3CoinGet; break;
            // }
            
            yield return new WaitForSecondsRealtime(0.5f);

            if (hasCoin)
            {
                flame.enabled = true;
                star.enabled = true;
            }
            else
            {
                flame.enabled = true;
            }
        }
        
        yield return new WaitForSecondsRealtime(0.5f);
        
        Color c2 = clickText.color;
        c2.a = 1.0f;
        clickText.color = c2;

        canTap = true; // フェードイン完了後にタップ待ち
    }
}
