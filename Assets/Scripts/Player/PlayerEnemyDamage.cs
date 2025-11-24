using UnityEngine;

public class PlayerEnemyDamage : MonoBehaviour
{
    public BatteryConfig config;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("敵に当たった");
            BatteryController.Instance.UseBattery(config.enemyContactDamage);
        }
    }
}
