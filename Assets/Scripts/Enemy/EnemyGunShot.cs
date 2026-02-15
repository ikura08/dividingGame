using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyGunShot : MonoBehaviour
{
    public bool isGround = true;
    private float timer;
    public GameObject bulletL;
    public GameObject bulletR;
    public GameObject bulletD;
    public SoundConfig soundConfig;
    public AudioSource bulletESource;
    public bool isUpsideDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.7f && isGround == true)
        {
            bulletESource.Play();
            if (!isUpsideDown)
            {
                ShootLeft();
                ShootRight();
            }
            if (isUpsideDown)
            {
                ShootDown();
            }
            timer = 0;
        }
    }

    void ShootLeft()
    {
        GameObject b = Instantiate(bulletL, transform.position, Quaternion.identity);
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.left * 7.2f, ForceMode2D.Impulse);
    }
    void ShootRight()
    {
        GameObject b = Instantiate(bulletR, transform.position, Quaternion.identity);
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * 7.2f, ForceMode2D.Impulse);
    }
    void ShootDown()
    {
        GameObject b = Instantiate(bulletD, transform.position, Quaternion.identity);
        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * 6.7f, ForceMode2D.Impulse);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            timer = 0f;
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
