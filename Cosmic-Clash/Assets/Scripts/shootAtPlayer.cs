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
    private Camera mainCamera;
    private Vector3 viewPos;
    void Start()
    {
        mainCamera = Camera.main;
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
        if (player != null && IsInView())
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }
    }
    private bool IsInView()
    {
        viewPos = mainCamera.WorldToViewportPoint(transform.position);
        return viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1;
    }
}