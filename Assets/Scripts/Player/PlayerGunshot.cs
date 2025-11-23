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
            // shotParticle.Emit(1);
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);

        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.AddForce(movement.lastDirection * 5f, ForceMode2D.Impulse);
    }
}
