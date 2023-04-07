using UnityEngine;

public class bossMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;
    public string difficulty = "easy";
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = pointA.position;
    }

    void Update()
    {
        if (difficulty == "hard"){
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed*2 * Time.deltaTime);

        if (transform.position == pointA.position)
        {
            targetPosition = pointB.position;
        }
        else if (transform.position == pointB.position)
        {
            targetPosition = pointA.position;
        }
        }
        else {transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == pointA.position)
        {
            targetPosition = pointB.position;
        }
        else if (transform.position == pointB.position)
        {
            targetPosition = pointA.position;
        }}
    }
}
