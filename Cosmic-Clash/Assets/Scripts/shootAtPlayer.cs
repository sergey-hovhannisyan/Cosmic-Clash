using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootAtPlayer : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float fireRate = 1f;

    private GameObject player;
    private float timeSinceLastFire;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeSinceLastFire = fireRate;
    }

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;

        if (timeSinceLastFire >= fireRate)
        {
            ShootBullet();
            timeSinceLastFire = 0f;
        }
    }

    private void ShootBullet()
    {
        if (player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }
    }
}