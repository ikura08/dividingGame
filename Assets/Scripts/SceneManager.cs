using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    Image panel;
    float currentAlpha = 0;
    bool isBlacking = false;
    Color panelColor;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        panelColor = panel.color;
        panelColor.a = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // StartCoroutine(nameof(Fade), 1);
            StartCoroutine(Fade(1f));
        }
    }

    public void Black()
    {

    }

    public IEnumerator Fade(float targetAlpha)
    {
        timer = 0;
        while (timer <= 2.0f)
        {
            timer += Time.deltaTime;
            float t = timer * 1 / 2;
            float a = Mathf.Lerp(currentAlpha, targetAlpha, t);
            panelColor.a = a;
            panel.color = panelColor;

            yield return null;
        }
        panelColor.a = targetAlpha;
        panel.color = panelColor;
    }
}
