using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : GunScript
{
    public Transform gunHolder;
    public Transform spawn;
    public GameObject bullet;
    public AudioClip rifleShotClip;

    float bulletSpeed = 2000f;
    float timeInterval = 0.07f;
    public bool isShooting = false;

    public override void Shoot(Vector3 fireDirection)
    {
        StartCoroutine(Fire(fireDirection));
    }

    private IEnumerator Fire(Vector3 direction)
    {
        GameObject newBullet = Instantiate(bullet, spawn.position, Quaternion.identity);
        newBullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        newBullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
        AudioSource.PlayClipAtPoint(rifleShotClip, spawn.position, 1f);
        yield return new WaitForSeconds(timeInterval);
    }
   
}
