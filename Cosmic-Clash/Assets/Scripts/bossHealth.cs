using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class bossHealth : MonoBehaviour
{
    public int totalHealth = 200;
    public float currentHealth = 200.0f;
    public TextMeshProUGUI healthText;
    public Image healthBar;  
    public GameManager _gameManager;

    private void Start() {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Bullet")){
            healthText.text = "Boss Health: " + (currentHealth - 1);
            healthBar.fillAmount = (currentHealth/totalHealth);
            currentHealth -=1;
            if (currentHealth <= 0){
                _gameManager.DecrementObjectiveCounter();
            }
            if (currentHealth <= 100){
                GetComponent<BossAI>().difficulty = "hard";
            }
        }
    }
}
