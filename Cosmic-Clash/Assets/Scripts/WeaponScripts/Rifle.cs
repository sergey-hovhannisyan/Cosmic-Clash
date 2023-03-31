using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : GunScript
{
    public Transform gunHolder;
    public Transform spawn;
    public GameObject bullet;
    public AudioClip rifleShotClip;
    private float timer;
    float bulletSpeed = 2000f;
    float timeInterval = 0.1f;
    public bool canFire = true;
    public float force = 20;
    

    // public override void Shoot(Vector3 fireDirection)
    // {
    //     StartCoroutine(Fire(fireDirection));
    // }

    // private IEnumerator Fire(Vector3 direction)
    // {
    //     GameObject newBullet = Instantiate(bullet, spawn.position, Quaternion.identity);
    //     newBullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    //     newBullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed);
    //     AudioSource.PlayClipAtPoint(rifleShotClip, spawn.position, 1f);
    //     yield return new WaitForSeconds(timeInterval);
    // }

    public override void Shoot(Vector3 direction)
    {
        StartCoroutine(Fire(direction));
    }

    private IEnumerator Fire(Vector3 direction)
    {
        if(canFire){
            canFire = false;
            GameObject newBullet = Instantiate(bullet, spawn.position, Quaternion.identity);
            Rigidbody2D bullet_rb = newBullet.GetComponent<Rigidbody2D>();
            bullet_rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            AudioSource.PlayClipAtPoint(rifleShotClip, spawn.position, 1f);
        }
        yield return 1;
    }

    void Update() {
        if(!canFire) {
            timer += Time.deltaTime;
            if(timer > timeInterval) {
                canFire = true;
                timer = 0;
            }
        }
    }
   
}
