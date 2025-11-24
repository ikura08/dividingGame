using UnityEngine;

public class BulletController : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public ParticleSystem onWallExplosionParticle;
    public ParticleSystem failedExplosionParticle;

    float lifeTime = 0.7f;
    bool isDead = false;
    
    void Start()
    {
        // 2秒後に自動で爆発扱い
        Invoke(nameof(ExplodeTime), lifeTime);
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
            ExplodeWall();
        }
    }

    void ExplodeEnemy()
    {
        isDead = true;
        ParticleSystem p = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(p.gameObject, 2f);

        Destroy(gameObject);
    }

    void ExplodeWall()
    {
        if (isDead) return;
        isDead = true;
        ParticleSystem p = Instantiate(onWallExplosionParticle, transform.position, Quaternion.identity);
        Destroy(p.gameObject, 2f);

        Destroy(gameObject);
    }
    
    void ExplodeTime()
    {
        if (isDead) return;
        isDead = true;
        ParticleSystem p = Instantiate(failedExplosionParticle, transform.position, Quaternion.identity);
        Destroy(p.gameObject, 2f);

        Destroy(gameObject);
    }
}
