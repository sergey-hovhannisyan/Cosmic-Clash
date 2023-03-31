using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class enemyBullet : MonoBehaviour
{
    GameObject player;
    GameManager _gameManager;
    void Start(){
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void  OnTriggerEnter2D(Collider2D other) {
        Console.WriteLine("This is C#");
        if (other.CompareTag("Player")){
            _gameManager.DecrementLives();
        }
        Destroy(gameObject);
    }
}
