using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    public Sprite greenPortal;
    private SpriteRenderer mySprite;
    GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update() {
        if(_gameManager.levelComplete){
            mySprite.sprite = greenPortal;
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player") && _gameManager.levelComplete){
            _gameManager.nextLevel();
        }
    }
}
