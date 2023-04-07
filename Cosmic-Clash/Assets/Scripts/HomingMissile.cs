using System.Collections;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    public float rotateSpeed = 100.0f;
    public float loopRadius = 1.0f;
    public int minLoops = 1;
    public int maxLoops = 5;
    public bool loopRight = true;
    public float centerOffsetRange = 3.0f;
    public float straightDistance = 4.0f;

    private int loops;
    private float loopAngle = 0.0f;
    public Vector3 loopCenter;

    public int angleChangingSpeed = 45;
    void Start()
    {
        StartCoroutine(GoStraight());
        player = GameObject.FindGameObjectWithTag("Player").transform;
        loops = Random.Range(minLoops, maxLoops + 1);
        StopAllCoroutines();
        SetNewLoopCenter();
        StartCoroutine(LoopyBehavior());
    }
    void SetNewLoopCenter()
    {
        float offsetX = Random.Range(-centerOffsetRange, centerOffsetRange);
        float offsetY = Random.Range(-centerOffsetRange, centerOffsetRange);
        if (loopRight){
            loopCenter = transform.position + transform.right * loopRadius + new Vector3(offsetX, offsetY, 0);
        }
        else{
            loopCenter = transform.position + -transform.right * loopRadius + new Vector3(offsetX, offsetY, 0);
        }
    }

    IEnumerator LoopyBehavior()
    {
        Vector3 axis = new Vector3(0,0,1);
         while (loops > 0)
        {
            loopAngle += rotateSpeed * Time.deltaTime;
            if (loopRight) transform.RotateAround(loopCenter, axis, -rotateSpeed * Time.deltaTime);
            else transform.RotateAround(loopCenter, axis, rotateSpeed * Time.deltaTime);
            if (loopAngle >= 360.0f)
            {
                loops--;
                loopAngle = 0.0f;
                SetNewLoopCenter();
            }

            yield return null;
        }

        StartCoroutine(HomingBehavior());
    }

    IEnumerator GoStraight()
    {
        float distanceTravelled = 0.0f;

        while (distanceTravelled < straightDistance)
        {
            float step = speed * Time.deltaTime;
            transform.position += transform.up * step;
            distanceTravelled += step;
            yield return null;
        }
    }
    IEnumerator HomingBehavior()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        while (true)
        {
            Vector2 direction = (Vector2)player.position - rigidBody.position;
            direction.Normalize ();
            float rotateAmount = Vector3.Cross (direction, transform.up).z;
            rigidBody.angularVelocity = -angleChangingSpeed * rotateAmount;
            rigidBody.velocity = transform.up * speed;
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("missile") && !other.CompareTag("EnemyBullet") && !other.CompareTag("Enemy")){
            Destroy(gameObject);
        }
    }
}
