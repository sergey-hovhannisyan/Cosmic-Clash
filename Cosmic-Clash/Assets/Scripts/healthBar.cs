using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthBar : MonoBehaviour
{
    public int totalHealth = 30; // maximum health is 30
    public Sprite[] healthBarSprites;
    private int currentHealth;
    private SpriteRenderer mySprite;
    void Start()
    {   
        currentHealth = totalHealth;
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.sprite = healthBarSprites[currentHealth];
    }

    public bool DecrementLives(){
        currentHealth-=1;
        if (currentHealth < 0) currentHealth= 0;
        mySprite.sprite = healthBarSprites[currentHealth];
        if (currentHealth <= 0){
            return true;
        }
        return false;
    }
}
