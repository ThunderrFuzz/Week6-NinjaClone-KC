using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemThrower : GameManager
{
    Rigidbody rb;
    public int pointValue;
    public GameObject[] explosionPrefabs;

    public AudioClip clip;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * Random.Range(1,15), ForceMode.Impulse);
        rb.AddTorque(randomTorque(), ForceMode.Impulse);
        transform.position = new Vector3(Random.Range(-5, 5), 0, 0);
    }

    Vector3 randomTorque()
    {
        //randomize spin direction 
        float randomx =Random.Range(1,15);
        
        return new Vector3(randomx, 0, 0);


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //make this on mouse down after dev onmouse enter == easier to test with 
    private void OnMouseEnter()
    {
        if (!isPaused)
        {
            //get postion of game object 
            Vector3 spawnPosition = gameObject.transform.position;
            //spawn particle at that location
            Instantiate(explosionPrefabs[Random.Range(0, explosionPrefabs.Length)], spawnPosition, Quaternion.identity);

            source = FindObjectOfType<AudioSource>();
            source.PlayOneShot(clip);

            //destroy gameobject
            Destroy(gameObject);

            if (gameObject.CompareTag("BadItem"))
            {
                lives--;
                addPoints(pointValue);
            }
            else
            {
                addPoints(pointValue);
            }
        }
        
    }
    
}
