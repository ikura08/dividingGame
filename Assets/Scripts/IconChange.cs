using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconChange : MonoBehaviour
{
    public RectTransform jumpIconFolder; // Jumpアイコン＋背景をまとめたフォルダ
    public RectTransform moveIconFolder; // Moveアイコン＋背景をまとめたフォルダ
    private bool isJumpLarge = true;
    public float timeElapsed;
    public float during = 0.5f;
    public bool isMoving = false;
    Vector2 startPosJ;
    Vector2 startPosM;
    Vector2 largePos = new Vector2(160f, 200f);
    Vector2 smallPos = new Vector2(350f, 100f);
    Vector2 largeScale = new Vector2(2f, 2f);
    Vector2 smallScale = new Vector2(1f, 01f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {

            timeElapsed += Time.deltaTime;
            float t = timeElapsed / during;

            if (isJumpLarge == true)
            {
                jumpIconFolder.anchoredPosition = Vector2.Lerp(largePos, smallPos, t);
                moveIconFolder.anchoredPosition = Vector2.Lerp(smallPos, largePos, t);

                jumpIconFolder.transform.localScale = Vector3.Lerp(largeScale, smallScale, t);
                moveIconFolder.transform.localScale = Vector3.Lerp(smallScale, largeScale, t);
            }
            else
            {
                jumpIconFolder.anchoredPosition = Vector2.Lerp(smallPos, largePos, t);
                moveIconFolder.anchoredPosition = Vector2.Lerp(largePos, smallPos, t);

                jumpIconFolder.transform.localScale = Vector3.Lerp(smallScale, largeScale, t);
                moveIconFolder.transform.localScale = Vector3.Lerp(largeScale, smallScale, t);
            }

            if (t > 1)
            {
                isMoving = false;
                timeElapsed = 0;
                isJumpLarge = !isJumpLarge;
            }
        }
    }

    public void IconChanging()
    {
        timeElapsed = 0f;
        isMoving = true;
    }
}
