using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LightMoving : MonoBehaviour
{
    public GameObject movingLight;
    public Renderer lightRenderer;
    public Material lightMat;
    public float maxRadius = 0.05f;
    public float minRadius = 0.02f;
    public float speed = 2f;
    public bool isPlayOn = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayOn == true)
        {
            float t = Time.deltaTime * speed;
            float radius = Mathf.Lerp(minRadius, maxRadius, (Mathf.Sin(t) + 1f) / 2f);
            lightMat.SetFloat("_Radius", radius);
        }
    }

    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("MovingLight"))
    //     {
    //         movingLight = collision.gameObject.GetComponent<GameObject>();
    //         lightRenderer = movingLight.GetComponent<Renderer>();
    //         lightMat = lightRenderer.GetComponent<Material>();
    //     }
    // }
    private void OCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingLight"))
        {
            movingLight = collision.gameObject;
            lightRenderer = movingLight.GetComponent<Renderer>();
            lightMat = lightRenderer.material;
            isPlayOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingLight"))
        {
            isPlayOn = false;
        }
    }
}
