using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerGunshot : MonoBehaviour
{
    public ParticleSystem shotParticle;
    public GameObject bullet;
    PlayerMovement movement;
    public BatteryConfig config;
    public BatteryController bController;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("残量は" + bController.currentBattery + "、コストは" + config.bulletCost);
            if (bController.currentBattery < config.bulletCost)
            {
                SceneManagerScr.Instance.GameOver();
            }
            else if (bController.currentBattery >= config.bulletCost)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);

        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.AddForce(movement.lastDirection * 7f, ForceMode2D.Impulse);

        BatteryController.Instance.UseBattery(config.bulletCost);
    }
}
