using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignMerge : MonoBehaviour
{
    public GameObject player;
    private bool isMerge = false;
    public GameObject textBackground;
    public GameObject signBackground;
    private Renderer backgroundRenderer;
    // Start is called before the first frame update
    void Start()
    {
        textBackground.SetActive(false);
        signBackground.SetActive(true);
        backgroundRenderer = textBackground.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(transform.position.x - player.transform.position.x);

        if (distance < 3.5f && !isMerge)
        {
            textBackground.SetActive(true);
        signBackground.SetActive(false);
            isMerge = true;
        }
        if (distance >= 3.5f && isMerge)
        {
            textBackground.SetActive(false);
        signBackground.SetActive(true);
            isMerge = false;
        }
    }

    IEnumerator backgroundMerge()
    {
        
        float t = 0;
        Color c = backgroundRenderer.material.color;

        if (c.a >= 1f)
            yield break;

        while (t < 0.2f)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / 0.2f);
            backgroundRenderer.material.color = c;
            yield return null;
        }
    }
}
