using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunScript
{
    public Transform gunHolder;
    public Transform spawn;
    public GameObject bullet;
    public AudioClip shotgunShotClip;
    public AudioClip shotGunLoadClip;
    private float timer;
    float timeInterval = 1f;
    public bool canFire = true;
    public int bullet_num = 10;
    public float force = 20f;

    public override void Shoot(Vector3 direction)
    {
        StartCoroutine(Fire(direction));
    }

    private IEnumerator Fire(Vector3 direction)
    {

        if (canFire){
            canFire = false;
            for(int i = 0; i < bullet_num; i++) {
                GameObject newBullet = Instantiate(bullet, spawn.position, Quaternion.identity);
                Rigidbody2D bullet_rb = newBullet.GetComponent<Rigidbody2D>();
                bullet_rb.velocity = new Vector2(direction.x * Random.Range(1f, 5f), direction.y * Random.Range(1f, 5f)).normalized * force;
            }
            AudioSource.PlayClipAtPoint(shotgunShotClip, spawn.position, 1f);
            yield return new WaitForSeconds(0.5f);
            AudioSource.PlayClipAtPoint(shotGunLoadClip, spawn.position, 1f);
        }
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
