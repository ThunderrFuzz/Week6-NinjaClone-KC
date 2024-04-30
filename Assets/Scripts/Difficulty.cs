using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    public AudioClip clip;
    AudioSource source3;
    public GameManager gm;
    public Button easy;
    public Button medium;
    public Button hard;
    // Start is called before the first frame update
    void Start()
    {
        gm.restartGameButton.SetActive(false);
        source3 = FindObjectOfType<AudioSource>();
    }

   
    public void easyDiff()
    {
        source3.PlayOneShot(clip);
        gm.spawnDelay = 1;
        
        gm.lives = 10;
        gm.startGame();
    }
    public void mediumDiff() 
    {
        source3.PlayOneShot(clip);
        gm.spawnDelay = .5f;
        gm.lives = 5;
        gm.startGame();
    }
    public void hardDiff() 
    {
        source3.PlayOneShot(clip);
        gm.spawnDelay = .25f;
        gm.lives = 2;
        gm.startGame();
    }
}
