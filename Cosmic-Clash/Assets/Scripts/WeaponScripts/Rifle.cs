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
    float timeInterval = 0.1f;
    public bool canFire = true;
    public float bulletForce = 35;

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
            bullet_rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletForce;
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
