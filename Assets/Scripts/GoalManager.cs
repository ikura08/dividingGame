using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GoalManager : MonoBehaviour
{
    private bool isCleared = false;
    public Text clearText;
    float fadeSpeed = 1f;
    bool canTap = false;
    public GameObject clearEffectPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canTap == true && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Select");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isCleared == false)
        {
            SelectStage.clearCount += 1;
            SelectStage.nextStage += 1;
            GameObject effect = Instantiate(clearEffectPrefab, transform.position, Quaternion.identity);
            isCleared = true;
            GetComponent<Renderer>().enabled = false;
            StartCoroutine(FadeInText());
        }
    }
    
    private IEnumerator FadeInText()
    {
        Color c = clearText.color;
        float a = 0f;
        while (a < 1f)
        {
            a += Time.deltaTime * fadeSpeed;
            c.a = a;
            clearText.color = c;
            yield return null;
        }
        canTap = true; // フェードイン完了後にタップ待ち
    }
}
