using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwipeTrail : MonoBehaviour
{
    public float swipeTrailWidth = 0.1f;
    public GameObject swipeTrailPrefab;
    private GameObject currentSwipeTrail;

    private LineRenderer lineRenderer;
    private Material trailMaterial;
    private Color startColor; 
    private Vector3 mousePos;

    void Start()
    {
        // grabs the matieral from a fancy instatniate 
        trailMaterial = Instantiate(swipeTrailPrefab.GetComponent<LineRenderer>().sharedMaterial);
        startColor = trailMaterial.color;
    }

    void Update()
    {
        // if m1 down start, on hold update, on realse end. 
        if (Input.GetMouseButtonDown(0))
        {
            StartSwipeTrail();
        }
        else if (Input.GetMouseButton(0))
        {
            UpdateSwipeTrail();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndSwipeTrail();
        }
    }


    void StartSwipeTrail()
    {
        //mouse pos grabber
        Vector3 mousePos = Input.mousePosition;

        // convert screen to world postion 
        float distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
        mousePos.z = distanceFromCamera;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // instantiates and creates the trail 
        currentSwipeTrail = Instantiate(swipeTrailPrefab, worldPos, Quaternion.identity);
        lineRenderer = currentSwipeTrail.GetComponent<LineRenderer>();
        
        //sets line renderer stuff
        lineRenderer.startWidth = swipeTrailWidth/3;
        lineRenderer.endWidth = swipeTrailWidth;
        lineRenderer.positionCount = 1; 
        lineRenderer.SetPosition(0, worldPos);

        // set trail colour to starting colour to reset the 
        trailMaterial.color = startColor;
    }



    void UpdateSwipeTrail()
    {
        if (currentSwipeTrail != null)
        {
            // get mouse pos
            Vector3 mousePos = Input.mousePosition;

            // convert screen to world postion 
            float distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
            mousePos.z = distanceFromCamera;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // adds new mouse postion before setting the line renderer to postion 
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);
        }
    }

    void EndSwipeTrail()
    {
        if (currentSwipeTrail != null)
        {
            //destroyes swipe trail
            Destroy(currentSwipeTrail, 1f);
        }
    }

    
}


