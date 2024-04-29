using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text livesUI;
    public TMP_Text gameOver_tmp;
    public TMP_Text scoreText;
    public TMP_Text paused;
    public Slider volSlider;
    public GameObject restartGameButton;
    public GameObject titleScreen;
    public AudioClip menuClip;
    [Header("Spawning")]
    public List<GameObject> SpawnedItems = new List<GameObject>();
    
    bool canSpawn;
    bool gameOver;
    protected static bool isPaused = false;
    public float spawnDelay;
    public int lives = 5;
    protected static int score;
    public static AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = FindObjectOfType<AudioSource>();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Toggle pause on ESC key press
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }



        if (!isPaused)
        {
            
            updateScore();
            changeVolume();
            if (lives <= 0)
            {
                lives = 0;
                gameOver = true;
            }
            livesUI.text = "Lives remaining: " + lives;
            if (gameOver)
            {
                stopGame();
                if (Input.GetKeyDown(KeyCode.R))
                {
                    restart();
                }
            }
        }
        

    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        // Display pause menu or pause screen here
        paused.text = "Paused";
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        // Hide pause menu or pause screen here
        paused.text = "Playing";
    }

    void stopGame()
    {
        //enables restart button
        restartGameButton.SetActive(true);
        
        // stops object spawning
        canSpawn = false;

        //pasue game
        Time.timeScale = 0;
       

        //display gameover
        
        gameOver_tmp.text = "Game Over. Press R To reset";
        
    }

    IEnumerator spawnObjects()
    {
        while (canSpawn)
        {
            
            yield return new WaitForSeconds(spawnDelay);
            int objectIndex = Random.Range(0, SpawnedItems.Count);
            Instantiate(SpawnedItems[objectIndex], transform);
        }
    }


    public void addPoints(int toadd)
    {
        score += toadd;
        
    }
    void updateScore()
    {
        scoreText.text = "Points Scored: " + score;
    }

    public void changeVolume()
    {
       
        AudioListener.volume = volSlider.value;
        source.PlayOneShot(menuClip);
    }
    public void restart()
    {
        source.PlayOneShot(menuClip);
        SceneManager.LoadScene("Prototype5");
    }

    public void startGame()
    {
        
        titleScreen.SetActive(false);
        restartGameButton.SetActive(false);
        canSpawn = true;
        Time.timeScale = 1;

        
        StartCoroutine(spawnObjects());
        
        score = 0;
    }
}
