using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float destroyDelay = 5f;

    void Start()
    {
        // Invoke("DestroyBullet", destroyDelay);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet") && !other.CompareTag("Enemy"))
            DestroyBullet();
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}