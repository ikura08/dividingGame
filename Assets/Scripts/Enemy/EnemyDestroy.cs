using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    public ParticleSystem destroyParticle;
    public BatteryConfig config;

    // Start is called before the first frame update
    void Start()
    {
        
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

            BatteryController.Instance.AddBattery(config.enemybatteryRecover);

            Destroy(gameObject);
        }
    }
}
