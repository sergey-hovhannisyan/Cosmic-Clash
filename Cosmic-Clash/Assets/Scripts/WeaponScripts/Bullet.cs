using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float destroyDelay = 2f;

    void Start()
    {
        Invoke("DestroyBullet", destroyDelay);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("EnemyBullet"))
            DestroyBullet();
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
