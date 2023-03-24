using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;
    public int jumpForce = 300;

    private Rigidbody2D _rigidbody;

    public LayerMask groundLevel;
    public Transform lFoot;
    public Transform rFoot;

    bool lGrounded = false;
    bool rGrounded = false;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
    }


    void Update()
    {
        lGrounded = Physics2D.OverlapCircle(lFoot.position, 0.3f, groundLevel);
        rGrounded = Physics2D.OverlapCircle(rFoot.position, 0.3f, groundLevel);

        if (Input.GetButtonDown("Jump") && (lGrounded || rGrounded))
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }
}
