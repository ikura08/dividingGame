using UnityEngine;

public class BulletController : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public ParticleSystem failedExplosionParticle;

    float lifeTime = 2f;
    bool isDead = false;
    
    void Start()
    {
        // 2秒後に自動で爆発扱い
        Invoke(nameof(ExplodeOther), lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            ExplodeEnemy();
        }
        else
        {
            ExplodeOther();
        }
    }

    void ExplodeEnemy()
    {
        isDead = true;
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void ExplodeOther()
    {
        if (isDead) return;
        isDead = true;
        Instantiate(failedExplosionParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
