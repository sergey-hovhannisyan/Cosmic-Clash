using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : GunScript
{
    public Transform gunHolder;
    public Transform spawn;
    public AudioClip laserShotClip;
    public ParticleSystem laserLoadup; 

    private float timer;
    float timeInterval = 1f;
    bool canFire = true;
    
    float distance = 30f;
    RaycastHit2D hit;
    GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public override void Shoot(Vector3 direction)
    {
        StartCoroutine(Fire(direction));
    }

    private IEnumerator Fire(Vector3 direction)
    {
        if (canFire)
        {
            canFire = false;
            ParticleSystem glore = Instantiate(laserLoadup, spawn.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(laserShotClip, spawn.position, 1f);
            hit = Physics2D.Raycast(spawn.position, direction, distance);
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
                _gameManager.DecrementObjectiveCounter();
            }
            yield return new WaitForSeconds(0.5f);
            Destroy(glore.gameObject);
        }
        yield return 1;
    }

    void Update()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeInterval)
            {
                canFire = true;
                timer = 0;
            }
        }
    }
}