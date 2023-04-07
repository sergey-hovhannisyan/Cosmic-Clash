using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallBorder : MonoBehaviour
{
    GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("player")){
            _gameManager.RestartScene();
        }
    }
}
