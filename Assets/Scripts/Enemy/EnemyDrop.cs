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
    private Vector2 thisPosition;
    public BatteryConfig config;
    public SoundConfig soundConfig;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRB = GetComponent<Rigidbody2D>();
        thisPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(transform.position.x - player.transform.position.x);

        if (distance < 2f && transform.position.y >= player.transform.position.y && isDrop == false)
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
            EnemyDropCreator.Instance.dropCreate(thisPosition);
        }

        if (collision.gameObject.CompareTag("Core"))
        {
            isDropping = false;
        }

        if (collision.gameObject.CompareTag("Player") && isDropping == true)
        {
            BatteryController.Instance.UseBattery(config.dropDamage);
            DamageEffect.Instance.FlashRed();
            AudioSource.PlayClipAtPoint(soundConfig.dropClip, transform.position);
            EnemyDropCreator.Instance.dropCreate(thisPosition);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}