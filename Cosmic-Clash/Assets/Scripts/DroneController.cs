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
    public float destroyDistance = 40f;
    private bool isFollowing = false;
    private bool delivered = false;


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
        if (!delivered && Vector2.Distance(dronePos, playerPos) < detectDistance) {
            isFollowing = true;
        }
        if (isFollowing) {
            transform.position = Vector2.MoveTowards(dronePos, Vector2.Lerp(dronePos, targetPos, time), speed);
        }
        if (!delivered) {
            weapon.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        }
        if (Vector2.Distance(dronePos, targetPos) < 0.2) {
            delivered = true;
            isFollowing = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
            weapon.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        if (Vector2.Distance(dronePos, playerPos) > destroyDistance) {
            Destroy(gameObject);
        }
    }
}
