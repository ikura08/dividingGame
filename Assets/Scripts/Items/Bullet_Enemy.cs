using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public ParticleSystem onWallExplosionParticle;
    public ParticleSystem failedExplosionParticle;

    float lifeTime = 0.9f;
    bool isDead = false;
    
    void Start()
    {
        Invoke(nameof(ExplodeTime), lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            ExplodePlayer();
        }
        else
        {
            ExplodeWall();
        }
    }

    void ExplodePlayer()
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
