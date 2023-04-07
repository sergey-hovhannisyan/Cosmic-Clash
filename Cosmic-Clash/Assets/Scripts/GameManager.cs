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
    public TextMeshProUGUI instructionText;
    public GameObject instructionMenu;


    private Vector3 mousePos;
    private Camera mainCam;
    public GameObject player;
    public GunController playerGunController;
    
    private void Awake() {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        if (level == -1) {
        }
        else {
            if (level == 0) {
                objectiveUI.text = "Enemies Remaining: " + objectiveCounter;
            }
            else if (level == 1) {
                objectiveUI.text = "Enemies Remaining: " + objectiveCounter;
                instructionText.text = "level0Instruction mission to kill and go for portal";
                OpenInstruction();
            }
            else if (level == 2) {
                objectiveUI.text = "Friends Remaining: " + objectiveCounter;
                instructionText.text = "level0Instruction";
                OpenInstruction();
            }
            levelUI.text = "Level " + level;
            livesUI.sprite = livesSprites[lives];
        }
    }

    public void DecrementObjectiveCounter() {
        objectiveCounter -= 1;
        if (level == 1) objectiveUI.text = "Enemies Remaining: " + objectiveCounter;
        else objectiveUI.text = "Friends Remaining: " + objectiveCounter;
        if (objectiveCounter <= 0) {
            levelComplete = true;
            objectiveUI.text = "Mission Complete";
        }
    }

    public void nextLevel() {
        if (levelComplete) {
            if (level == -1) {
                SceneManager.LoadScene("Tutorial");
                level = 0;
            }
            else if (level == 0) {
                SceneManager.LoadScene("Level1");
                level = 1;
            }
            else if (level == 1) {
                SceneManager.LoadScene("Level2");
                level = 2;
            }
            else if (level == 2) {
                SceneManager.LoadScene("Level3");
                level = 3;
            }
            else {
                SceneManager.LoadScene("Win");
            }
            Destroy(gameObject);
        }
    }
    public void DecrementLives() {
        lives -= 1;
        if (lives <= 0) {
            SceneManager.LoadScene("Game Over");
            Destroy(gameObject);
            lives = 0;
        }

        livesUI.sprite = livesSprites[lives];
    }

    public void IncrementLives(){
        lives += 1;
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
        Time.timeScale = 1f;
        Destroy(gameObject);

    }
    public void Restart(){
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
    public void Quit(){
        Application.Quit();
    }

    // GunController interface methods
    private Vector3 MousePos()
    {
        return mainCam.ScreenToWorldPoint(Input.mousePosition);
    }

    public Vector3 PlayerPosition()
    {
        return player.transform.position;
    }
    
    //Instruction methods
    public void OpenInstruction()
    {
        instructionMenu.SetActive(true);
    }
    public void OpenInstruction(string text)
    {
        instructionMenu.SetActive(true);
        instructionText.text = text;
    }

    public void CloseInstruction()
    {
        instructionMenu.SetActive(false);
    }
}
