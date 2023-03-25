using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    void Start()
    {
    }

    void Update()
    {
        Vector3 newPosition = new Vector3(playerTransform.position.x, playerTransform.position.y+2f, transform.position.z);
        transform.position = newPosition;
    }
}
