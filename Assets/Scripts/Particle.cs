using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public ParticleSystem psBurst;
    public ParticleSystem psCircle;

    public float holdDuration = 0.5f; // 最大までの時間
    public float minRate = 10f;
    public float maxRate = 1000f;

    private float holdTime = 0f;
    private ParticleSystem.EmissionModule emission;
    // Start is called before the first frame update
    void Start()
    {
        emission = psBurst.emission;
        emission.rateOverTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            holdTime += Time.deltaTime;
            float t = Mathf.Clamp01(holdTime / holdDuration); // 0～1に補正
            float newRate = Mathf.Lerp(minRate, maxRate, t);
            emission.rateOverTime = newRate;

            if (psBurst.isStopped) psBurst.Play();
        }
        else if (Input.GetKeyUp(KeyCode.Y))
        {
            // ボタン離したらリセット（必要なら）
            psBurst.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            holdTime = 0f;
            emission.rateOverTime = 0;
            psCircle.Play();
        }
    }
}
