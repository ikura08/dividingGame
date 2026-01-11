using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] BatteryController batteryController;
    private Vector3[] lightSizes = new Vector3[3];
    
    [Header("Light Settings")]
    public Light2D mainLight;
    public Light2D subLight;
    public float changeDuration = 0.5f;

    private Coroutine resizeCoroutine;
    private int lastLevel = 2;
    private int initialLevel = 2;

    void Start()
    {
        lightSizes[0] = new Vector3(1.1f, 0.88f, 1f);
        lightSizes[1] = new Vector3(1.5f, 1.2f, 1f);
        lightSizes[2] = new Vector3(2.0f, 1.6f, 1f);

        if (batteryController.currentBattery <= 100) 
        {
            initialLevel = 0;
        }
        else if (batteryController.currentBattery <= 200) 
        {
            initialLevel = 1;
        }
        else initialLevel = 2;

        lastLevel = initialLevel;

        // Vector3 startSize = lightSizes[initialLevel];
        // mainLight.transform.localScale = startSize;
        // subLight.transform.localScale = startSize * 1.3f;
    }

    public void ChangeLightSize(int level)
    {
        lastLevel = level;

        if (resizeCoroutine != null) 
        {
            StopCoroutine(resizeCoroutine);
        }
        resizeCoroutine = StartCoroutine(AnimateLight(lightSizes[level]));
    }

    IEnumerator AnimateLight(Vector3 targetSize)
    {
        Vector3 startSize = mainLight.transform.localScale;

        float time = 0f;

        while (time < changeDuration)
        {
            time += Time.deltaTime;
            float t = time / changeDuration;

            Vector3 currentSize = Vector3.Lerp(startSize, targetSize, t);
            mainLight.transform.localScale = currentSize;
            subLight.transform.localScale = currentSize * 1.3f;

            yield return null;
        }

        mainLight.transform.localScale = targetSize;
        subLight.transform.localScale = targetSize * 1.3f;
    }
}