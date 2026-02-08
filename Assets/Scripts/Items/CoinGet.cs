using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGet : MonoBehaviour
{
    public SoundConfig soundConfig;
    public int stageNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CoinData.isStageCoinGet[stageNumber - 1] = true;
            AudioSource.PlayClipAtPoint(soundConfig.coinClip, transform.position);
            Destroy(gameObject);
        }
    }
}
