using UnityEngine;

public class bossMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = pointA.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == pointA.position)
        {
            targetPosition = pointB.position;
        }
        else if (transform.position == pointB.position)
        {
            targetPosition = pointA.position;
        }
    }
}
