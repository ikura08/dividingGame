using UnityEngine;

public class PlayerEnemyDamage : MonoBehaviour
{
    public BatteryConfig config;
    public SoundConfig soundConfig;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(soundConfig.damagePClip, transform.position);
            BatteryController.Instance.UseBattery(config.enemyContactDamage);
            BatteryController.Instance.OnDamage();

            if (CameraShake.Instance != null)
            {
                CameraShake.Instance.Shake(0.2f, 0.1f);
            }
        }
    }
}