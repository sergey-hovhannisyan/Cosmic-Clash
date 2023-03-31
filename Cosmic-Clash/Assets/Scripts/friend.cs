using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class friend : MonoBehaviour
{
    GameManager _gameManager;

    void Start() {
        _gameManager = FindObjectOfType<GameManager>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _gameManager.DecrementObjectiveCounter();
            Destroy(gameObject);
        }
    }
}
