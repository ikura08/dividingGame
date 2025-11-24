using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    public ParticleSystem destroyParticle;

    public int enemyBatteryRecover = 12;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("始まった");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            ParticleSystem p = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(p, 2f);

            BatteryController.Instance.UseBattery(enemyBatteryRecover);

            Destroy(gameObject);
        }
    }
}
