using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    public TMP_Text clickText;
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // float alpha = Mathf.Lerp(0.2f, 1f, (Mathf.Cos(Time.time * speed) + 1f) / 2f);
        float alpha = Mathf.PingPong(Time.time + speed, 0.9f) + 0.1f;

        Material mat = clickText.fontMaterial;
        Color face = mat.GetColor("_FaceColor");
        Color outline = mat.GetColor("_OutlineColor");
        Color glow = mat.GetColor("_GlowColor");
        face.a = outline.a = glow.a = alpha;
        mat.SetColor("_FaceColor", face);
        mat.SetColor("_OutlineColor", outline);
        mat.SetColor("_GlowColor", glow);
    }
}
