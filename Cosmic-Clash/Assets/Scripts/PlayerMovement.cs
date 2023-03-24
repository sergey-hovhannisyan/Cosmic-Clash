using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int jumpHeight = 20;
    public int moveSpeed = 10;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX* moveSpeed, rb.velocity.y);
        
        if(Input.GetKeyDown("space")){
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }
}
