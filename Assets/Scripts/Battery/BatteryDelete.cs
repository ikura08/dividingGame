using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryDelete : MonoBehaviour
{
    bool picked = false;
    public BatteryConfig config;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (picked) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            picked = true;
            BatteryController.Instance.AddBattery(config.batteryPickupRecover);
            Destroy(gameObject);
        }
    }
}
