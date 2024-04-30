using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.VisualScripting.Member;

public class ItemThrower : MonoBehaviour
{
    GameManager gm;
    Combo ComboManager;
    Rigidbody rb;
    public int pointValue;
    public GameObject[] explosionPrefabs;

    public AudioClip clip;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        ComboManager = FindObjectOfType<Combo>();
        gm = FindAnyObjectByType<GameManager>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * Random.Range(6,25), ForceMode.Impulse);
        rb.AddTorque(randomTorque(), ForceMode.Impulse);
        transform.position = new Vector3(Random.Range(-5, 5), 0, 0);
    }

    Vector3 randomTorque()
    {
        //randomize spin direction 
        float randomx =Random.Range(1,15);
        float randomz = Random.Range(1,15); 
        return new Vector3(randomx, 0, randomz);


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //make this on mouse down after dev onmouse enter == easier to test with 
    private void OnMouseDown()
    {
        if (!gm.isPaused)
        {
            //get postion of game object 
            Vector3 spawnPosition = gameObject.transform.position;
            //spawn particle at that location
            Instantiate(explosionPrefabs[Random.Range(0, explosionPrefabs.Length)], spawnPosition, Quaternion.identity);

            

            //destroy gameobject
            Destroy(gameObject);
            source = FindObjectOfType<AudioSource>();
            if (gameObject.CompareTag("RandomMulti"))
            {
                int randomIncrease = Random.Range(3, 30);
                for (int i = 0; i < randomIncrease; i++)
                {
                    ComboManager.IncreaseCombo();
                }
            }
            if (gameObject.CompareTag("BadItem"))
            {
                ComboManager.ResetComboMultiplier();
                ComboManager.ResetCombo();
                gm.lives--;
                source.PlayOneShot(clip);
                gm.addPoints(pointValue);
            }
            else
            {
                ComboManager.IncreaseCombo();
                gm.addPoints(pointValue);
                source.PlayOneShot(clip);
            }
        }
        
    }
    
}
