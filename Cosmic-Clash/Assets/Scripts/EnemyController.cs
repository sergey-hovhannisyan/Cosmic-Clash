using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int speed = 5;
    public int jumpForce = 1500;
    public float jumpProbability = 0.3f;
    public float shootProbability = 1f;
    public float keepDistance = 2f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    public Transform _playerTransform;
    public Transform gunHolderTransform;
    public GunController gunController;
    private bool isHit = false;
    private int _lives = 3;

    private GameManager _gameManager;

    public LayerMask groundLevel;
    public Transform lFoot;
    public Transform rFoot;
    public bool lGrounded = false;
    public bool rGrounded = false;
    private bool _isFacingRight = true;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(RandomJump());
        StartCoroutine(RunToPlayer());
        StartCoroutine(RandomShoot());
    }

    private void Update()
    {
        lGrounded = Physics2D.OverlapCircle(lFoot.position, 0.4f, groundLevel);
        rGrounded = Physics2D.OverlapCircle(rFoot.position, 0.4f, groundLevel);

        // Check if the player is in sight
        Vector3 rotation = _playerTransform.position - gunHolderTransform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;


        // Changing Character direction
        if (_playerTransform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            gunHolderTransform.rotation = Quaternion.Euler(0, 0, rotZ - 90);
        }
        else if (_playerTransform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, -1, 1);
            gunHolderTransform.rotation = Quaternion.Euler(0, 0, rotZ + 90);
        }
    }

    void LateUpdate()
    {
        if (lGrounded || rGrounded)
        {
            _animator.SetBool("isJumping", false);
        }
    }

    private IEnumerator RandomShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            //// Random Shoot
            if (Random.value < shootProbability)
            {
                gunController.Shoot(_gameManager.PlayerPosition());
            }
        }
    }

    private IEnumerator RunToPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            Vector3 direction = _playerTransform.position - transform.position;
            Vector3 desiredPos = _playerTransform.position - direction.normalized * keepDistance;
            direction = desiredPos - transform.position;
            Vector2 velocity = direction.normalized * speed;
            _rigidbody.velocity = velocity;
            _animator.SetFloat("Speed", Mathf.Abs(velocity.x));
        }
    }



    private IEnumerator RandomJump()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            // Jump randomly
            if (Random.value < jumpProbability && (lGrounded || rGrounded))
            {
                _rigidbody.AddForce(new Vector2(0, jumpForce));
                _animator.SetBool("isJumping", true);
            }
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("JumpPad"))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, other.gameObject.GetComponent<JumpPad>().jumpHeight);
        }

        // Change the enemy lives and use healthbar
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            _lives--;
            if (_lives <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(WaitForBulletToPass(other));
    }

    private IEnumerator WaitForBulletToPass(Collider2D other)
    {
        if (other.CompareTag("Bullet") && !isHit)
        {
            isHit = true;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(0.2f);
        isHit = false;
    }
}
