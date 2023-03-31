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

    public bool lGrounded = false;
    public bool rGrounded = false;

    public Transform gunHolderTrans;
    public Transform headTrans;
    private Camera mainCam;
    private Vector3 mousePos;
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        _animator.SetFloat("Speed", Mathf.Abs(xSpeed));
    }


    void Update()
    {
        lGrounded = Physics2D.OverlapCircle(lFoot.position, 0.4f, groundLevel);
        rGrounded = Physics2D.OverlapCircle(rFoot.position, 0.4f, groundLevel);

        if (Input.GetButtonDown("Jump") && (lGrounded || rGrounded))
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce));
            _animator.SetBool("isJumping", true);
        }
        



        // Rotating gun holder
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - gunHolderTrans.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        

        //Changing Character direction
        if (mousePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            gunHolderTrans.rotation = Quaternion.Euler(0, 0, rotZ - 90);
        }
        else if (mousePos.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, -1, 1);
            gunHolderTrans.rotation = Quaternion.Euler(0, 0, rotZ + 90);
        }
    }
    void LateUpdate()
    {
        if (lGrounded || rGrounded)
        {
            _animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("JumpPad"))
        {
            _rigidbody.AddForce(new Vector2(0, 1650));
        }
    }
}