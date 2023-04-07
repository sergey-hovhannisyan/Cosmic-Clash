using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    // Start is called before the first frame update
    Transform Player;
    int nextAttack = 0;
    public Transform[] spawnPoints;
    public Transform enemySpawner;
    public Transform[] bulletSpawners;
    public Transform[] missileSpawners;
    public Transform[] patrolPoints;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;
    public GameObject enemyShipPrefab;
    public GameObject enemyPrefab;
    public int bulletSpeed;
    public int missileSpeed;
    public string difficulty = "easy";
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Attack();
    }
    void Attack(){
        StopAllCoroutines();
        switch(nextAttack){
            case 0:
                // StartCoroutine(SpawnShips(4,0.3f,1.0f));
                if (difficulty == "hard") StartCoroutine(FireMissiles(7,0.3f,0.9f));
                else StartCoroutine(FireMissiles(4,0.3f,1.0f));
                break;
            case 1:
                if (difficulty == "hard") StartCoroutine(ShootCone(7,0.3f,0.6f));
                StartCoroutine(ShootCone(4,0.3f,0.7f));
                break;
            case 2:
                if (difficulty == "hard") StartCoroutine(SpawnEnemies(6,0.3f,1.0f));
                else StartCoroutine(SpawnEnemies(3,0.3f, 1.5f));
                break;
            case 3:
                if (difficulty == "hard") StartCoroutine(SpawnShips(6,0.3f,0.9f));
                else StartCoroutine(SpawnShips(4,0.3f,1.0f));
                break;
            default:
                StartCoroutine(Idle());
                break;
        }
        nextAttack += 1;
        nextAttack = nextAttack % 4;
    }

    IEnumerator Idle(){
        yield return new WaitForSeconds(1);
        Attack();
    }

    IEnumerator ShootCone(int numWaves, float reload,float delay){
        yield return new WaitForSeconds(delay);
        for (int i = 0; i <numWaves; i++ ){
            foreach (Transform point in bulletSpawners)
            {
                GameObject newBullet = Instantiate(bulletPrefab,
                    point.position,point.rotation);
                newBullet.GetComponent<Rigidbody2D>().AddForce(point.up * bulletSpeed);
            }
            yield return new WaitForSeconds(reload);
        }
        yield return StartCoroutine(Idle());
        Attack();
    }
    IEnumerator FireMissiles(int num, float reload,float delay){
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < num; i++){
            for (int j = 0; j < missileSpawners.Length; j++){
                Transform point = missileSpawners[j];
                GameObject missile = Instantiate(missilePrefab,
                    point.position,point.rotation);
                if(j == 0){
                    missile.GetComponent<HomingMissile>().loopRight = false;
                }
                missile.GetComponent<Rigidbody2D>().AddForce(point.up*missileSpeed, ForceMode2D.Impulse);
                yield return new WaitForSeconds(reload);
            }
        }
        yield return StartCoroutine(Idle());
        Attack();
    }
    IEnumerator SpawnShips(int num, float reload, float delay){
        yield return new WaitForSeconds(delay);
        int index = 0;
        foreach (Transform point in spawnPoints)
        {
            GameObject newShip = Instantiate(enemyShipPrefab,
                point.position,point.rotation);
            newShip.GetComponent<Rigidbody2D>().AddForce(point.right * bulletSpeed,ForceMode2D.Impulse);
            newShip.GetComponent<patrol>().waypoints[0] = patrolPoints[index];
            newShip.GetComponent<patrol>().waypoints[1] = patrolPoints[index+1];
            index+=2;
            index = index % patrolPoints.Length;
             yield return new WaitForSeconds(reload);
        }
        yield return StartCoroutine(Idle());
        Attack();
    }
    IEnumerator SpawnEnemies(int num, float reload,float delay){
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < num; i++){
            GameObject newEnemy = Instantiate(enemyPrefab,enemySpawner.position,enemySpawner.rotation);
            yield return new WaitForSeconds(reload);
        }
        yield return StartCoroutine(Idle());
        Attack();
    }
}
