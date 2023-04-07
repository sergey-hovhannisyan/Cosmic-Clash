using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float shipDimensions = 0.5f;
    private int _currentWaypointIndex = 0;
    private float _speed = 2f;
    private Vector3 scaleChange = new Vector3(-0.1f, 0.0f, 0.0f);
    private void Update()
    {
        Transform wp = waypoints[_currentWaypointIndex];
        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
             StartCoroutine(rotate());
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, wp.position, _speed * Time.deltaTime);
        }
    }
     private IEnumerator rotate()
    {
        float elapsedTime = 0f;
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 180, 0);

        while (transform.localScale.x <= shipDimensions && transform.localScale.x >= -shipDimensions)
        {
            transform.localScale += scaleChange;
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / 3f;
            yield return null;
        }
        transform.localScale += -scaleChange;
        scaleChange.x = -scaleChange.x;
    }

}
