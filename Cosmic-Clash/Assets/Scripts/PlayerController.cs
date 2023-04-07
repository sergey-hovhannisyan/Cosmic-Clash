using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 15;
    public int jumpForce = 1200;

    GameManager _gameManager;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    public LayerMask groundLevel;
    public Transform lFoot;
    public Transform rFoot;

    bool lGrounded = false;
    bool rGrounded = false;

    private bool isHit = false;
    public Transform gunHolderTrans;
    public Transform headTrans;
    private Camera mainCam;
    private Vector3 mousePos;
    public GunController gunController;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
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
        

        // Changing Character direction
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

        // Swap Guns
        if (Input.GetKeyDown(KeyCode.E))
        {
            gunController.RightGunSwap();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            gunController.LeftGunSwap();
        }

        // Shoot
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot(mousePos);
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
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, other.gameObject.GetComponent<JumpPad>().jumpHeight);
        }
        if (other.gameObject.CompareTag("rifle"))
        {
            gunController.UnlockWeapon("rifle");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("shotgun"))
        {
            gunController.UnlockWeapon("shotgun");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("laser"))
        {
            gunController.UnlockWeapon("laser");
            Destroy(other.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(WaitForBulletToPass(other));
    }
    
    private IEnumerator WaitForBulletToPass(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet") && !isHit)
        {
            isHit = true;
            Destroy(other.gameObject);
            _gameManager.DecrementLives();
        }
        else if (other.CompareTag("Enemy")){
            isHit = true;
            other.GetComponent<enemyHealth>().DecrementLives();
            _gameManager.DecrementLives();
        }
        yield return new WaitForSeconds(0.2f);
        isHit = false;
    }
}