using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public GameObject healthBar;
    GameManager _gameManager;
    void Start(){
        _gameManager = FindObjectOfType<GameManager>();
    }
    public bool DecrementLives(){
        return healthBar.GetComponent<healthBar>().DecrementLives();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("here");
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (DecrementLives()) {
                _gameManager.DecrementObjectiveCounter();
                Destroy(gameObject);
            };
            Destroy(other.gameObject);
        }
    }
}
