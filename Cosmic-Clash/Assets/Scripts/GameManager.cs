using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
   // Create Interfaces for the classes you need and use GameManager as interface
   // in all other classes. Do not directly include enemy in player or thing like that. 
   // Make sure to find the object using code rather than assigning the scene. It will reduce the 
   // amount of work for creating a new scene. Also, keep in mind as we grow we might come up with core
   // so we can drag and drop in scenes and have complete game. 
   // Please, be descriptive: function -> does something to something = startPlayerShootingAnimation()
   // Don't worry if the function or variable names are long :)
   // HAVE FUN!
   public int lives;
   public int level;
   public bool levelComplete;
   public TextMeshProUGUI levelUI;
   public Image livesUI;
   public Sprite[] livesSprites;
   public GameObject pauseMenu;
   public TextMeshProUGUI objectiveUI;
   public int objectiveCounter;
    private void Awake() {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start(){
        if (level == -1){
        }
        else {
            if (level == 1){
                objectiveUI.text = "Enemies Remaining: " + objectiveCounter;
            }
            levelUI.text = "Level " + level;
            livesUI.sprite = livesSprites[lives];
        }
    }
    public void DecrementObjectiveCounter(){
        objectiveCounter-=1;
        if (level == 1) objectiveUI.text = "Enemies Remaining: " + objectiveCounter;
        else objectiveUI.text = "Friends Remaining: " + objectiveCounter;
        if(objectiveCounter == 0){
            levelComplete = true;
            objectiveUI.text = "Mission Complete";
        }
    }
    public void nextLevel(){
        if (levelComplete){
            if (level == -1){
                SceneManager.LoadScene("Level1");
            }
            else if (level == 0){
                SceneManager.LoadScene("Level1");
            }
            else if (level == 1) {
                SceneManager.LoadScene("Level2");
            }
            else if (level == 2) {
                SceneManager.LoadScene("Level3");
            }
            else {
                SceneManager.LoadScene("WinScreen");
            }
        Destroy(gameObject);
        }
    }
    public void DecrementLives(){
        lives -=1;
        if (lives == 0){
            SceneManager.LoadScene("Game Over");
        }
        livesUI.sprite = livesSprites[lives];
    }
    public void IncrementLives(){
        lives +=1;
        lives = Math.Min(lives,livesSprites.Length-1);
        livesUI.sprite = livesSprites[lives];
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Home()
    {
        SceneManager.LoadScene("Welcome");
        Destroy(gameObject);

    }
    public void Restart(){
        SceneManager.LoadScene("Level1");
        Destroy(gameObject);
    }
}
