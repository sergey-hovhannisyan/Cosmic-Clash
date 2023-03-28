using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;
    public int jumpForce = 1000;

    private Rigidbody2D _rigidbody;
    private Animator _animator; 

    public LayerMask groundLevel;
    public Transform lFoot;
    public Transform rFoot;

    bool lGrounded = false;
    bool rGrounded = false;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //_animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        //_animator.SetFloat("Speed", Mathf.Abs(xSpeed));
    }


    void Update()
    {
        lGrounded = Physics2D.OverlapCircle(lFoot.position, 0.3f, groundLevel);
        rGrounded = Physics2D.OverlapCircle(rFoot.position, 0.3f, groundLevel);

        if (Input.GetButtonDown("Jump") && (lGrounded || rGrounded))
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce));
            //_animator.SetBool("isJumping", true);
        }

        //if (lGrounded || rGrounded)
            //_animator.SetBool("isJumping", false);

    }
}
