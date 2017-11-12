using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    public float speed;

    public Vector3 nextPosition;
    public bool isMoving;
    private List<GameObject> pointHistory;
    private List<Vector3> nextPoints;

    public GameObject lightOrb;

    private bool hasFire;
    private GameObject gameStart;
    public EventTrigger gameStartTrigger;
    public GameObject portal;

    public GameObject controller;

    private GameObject[] hintOrbs;

    public bool puzzleStart;

    public GameObject island;
    public GameObject endRoom;



    // Use this for initialization
    void Start () {

        //Get HintOrb objects
        hintOrbs = GameObject.FindGameObjectsWithTag("HintOrb");
        for(int h=0; h < 3; h++)
        {
            hintOrbs[h].SetActive(false);
        }
        puzzleStart = false;

        //Get the Game Start and save it
        gameStart = GameObject.Find("GameStart");

        //Supress the event trigger while the game is not started
        gameStartTrigger.enabled = false;

        //Disable controller has long as we don't need it
        controller.SetActive(false);

        isMoving = false;
        pointHistory = new List<GameObject>();

        hasFire = false;

        nextPoints = new List<Vector3>();
        nextPoints.Add(new Vector3((float)5.2, 6, (float)-13.7));
        nextPoints.Add(new Vector3((float)0.98, 6, (float)-14.8));
        nextPoints.Add(new Vector3((float)-3.15, 6, (float)-12.82)); 
        nextPoints.Add(new Vector3((float)-7.07, 6, (float)-12.09));
        nextPoints.Add(new Vector3((float)-6.78, 6, (float)-9.46));
        nextPoints.Add(new Vector3((float)-7.25, 6, (float)-6.48));
        nextPoints.Add(new Vector3((float)-7, (float) 6.1, (float)-4.12));
        nextPoints.Add(new Vector3((float)-6.55, (float)6.1, (float)2.58));
        nextPoints.Add(new Vector3((float)-1.51, (float)6, (float)5.22));
        nextPoints.Add(new Vector3((float)1.27, (float)6, (float)6.96));
        //Useless adding
        nextPoints.Add(new Vector3((float)-7, (float)6.1, (float)-4.12));
        

    }
	
	// Update is called once per frame
	void Update () {

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
            if(this.transform.position == nextPosition)
            {
                isMoving = false;
                int i = (int) pointHistory.Count - 1;

                //Destroy the Collider of the actual point to avoid bad pointer interraction
                Destroy(pointHistory[i].GetComponent<Collider>());

                switch (i)
                {
                    case 7:
                        // StartGame
                        gameStartTrigger.enabled = true;
                        break;

                    case 10:
                        portal.AddComponent<SphereCollider>();
                        portal.GetComponent<SphereCollider>().radius = (float)1.5;
                        break;

                    default:
                       
                        //Add Next Point
                        addNextPoint(nextPoints[i], i);
                        break;
                }
  

                // Delete previous Light Orbs
                if (i > 2)
                {
                    pointHistory[i - 2].SetActive(false);
                }

                //To keep the game light we can supress useless LightOrb
                //To implement


            }
        } else if(puzzleStart)
        {
            if (isFireSet())
            {
                //Continue the path
                int i = (int)pointHistory.Count - 1;
                
                //Destroy the Collider of the actual point to avoid bad pointer interraction
                Destroy(pointHistory[i].GetComponent<Collider>());

                //Add Vortex
                portal.SetActive(true);

                //Add Next Point
                addNextPoint(nextPoints[i], i);
                puzzleStart = false;
            }
        }

    }

    public void moveToPoint(GameObject point) {

        if(pointHistory.Contains(point))
        {
            //Do nothing the point has already been visited
        }
        else
        {
           if (!isMoving)
           {
                pointHistory.Add(point);
                nextPosition = point.transform.position;
                isMoving = true;
           }
       }    
    }

    public void addNextPoint(Vector3 v, int i)
    {
        GameObject nextLightOrb = Instantiate(lightOrb, v, Quaternion.identity);

        //Add a Rigibody to the component
        nextLightOrb.AddComponent<Rigidbody>();
        nextLightOrb.GetComponent<Rigidbody>().useGravity = false;
        
        //Add a collider if needed
        if (i != 0) {
            nextLightOrb.AddComponent<SphereCollider>();
            nextLightOrb.GetComponent<SphereCollider>().radius = (float)0.4;
        }

        //Reset name
        nextLightOrb.name = "Light Orb " + i;

        //Add it to the path
        nextLightOrb.transform.parent = GameObject.Find("Path").transform;


    }

    public bool isFireSet(){
        bool ret = true;

        for (int j = 0; j<3 ; j++)
        {
            if (hintOrbs[j].activeSelf)
            {
                ret = false;
            }
        }

        return ret;

    }

    public void setPuzzleStart(bool state)
    {
        puzzleStart = state;
    }

    public void teleportAtEnd()
    {
        endRoom.SetActive(true);
        island.SetActive(false);
        this.transform.position = new Vector3((float)75, (float)-18.4, (float)118.5);
    }

}
