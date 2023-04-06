using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public GameObject weapon;
    public Transform player;
    public float time = 0.05f;
    public float speed = 0.15f;
    public float detectDistance = 20f;
    private bool isFollowing = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 dronePos = transform.position;
        Vector2 playerPos = player.position;
        Vector2 targetPos = playerPos + new Vector2(0, 5f);
        if (Vector2.Distance(dronePos, playerPos) < detectDistance) {
            isFollowing = true;
        }
        if (isFollowing) {
            transform.position = Vector2.MoveTowards(dronePos, Vector2.Lerp(dronePos, targetPos, time), speed);
        }
        if (Vector2.Distance(dronePos, targetPos) < 0.2) {
            //Destroy(gameObject);
        }
    }
}
