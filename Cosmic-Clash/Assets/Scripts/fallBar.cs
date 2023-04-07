using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallBar : MonoBehaviour
{
    public GameManager _gameManager;

    private void Start() {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            Destroy(other.gameObject);
            _gameManager.RestartScene();

        }
    }
}
