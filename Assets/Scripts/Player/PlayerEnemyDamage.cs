using UnityEngine;

public class PlayerEnemyDamage : MonoBehaviour
{
    public BatteryConfig config;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BatteryController.Instance.UseBattery(config.enemyContactDamage);

            if (CameraShake.Instance != null)
            {
                CameraShake.Instance.Shake(0.2f, 0.1f);
            }
        }
    }
}