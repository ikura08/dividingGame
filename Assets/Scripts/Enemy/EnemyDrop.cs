using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    private Rigidbody2D enemyRB;
    private bool isDrop = false;
    private bool isDropping = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - player.transform.position.x < 2f && transform.position.x - player.transform.position.x > -2f && isDrop == false)
        {
            Drop();
            isDrop = true;
            isDropping = true;
        }
    }

    void Drop()
    {
        enemyRB.bodyType = RigidbodyType2D.Dynamic;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isDropping = false;
            Destroy(gameObject, 2f);
        }

        if (collision.gameObject.CompareTag("Core"))
        {
            isDropping = false;
        }

        if (collision.gameObject.CompareTag("Player") && isDropping == true)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}