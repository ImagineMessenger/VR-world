using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    public int speed;

    public Vector3 nextPosition;
    public bool isMoving;
    private List<GameObject> pointHistory;
    private List<Vector3> nextPoints;

    public GameObject lightOrb;

    private bool hasFire;
    private GameObject gameStart;
    public EventTrigger gameStartTrigger;
    private GameObject portal;


    // Use this for initialization
    void Start () {

        //Get the Game Start and save it
        gameStart = GameObject.Find("GameStart");
        portal = GameObject.Find("Portal");

        //Supress the event trigger while the game is not started
        gameStartTrigger.enabled = false;


        isMoving = false;
        pointHistory = new List<GameObject>();

        hasFire = false;

        nextPoints = new List<Vector3>();
        nextPoints.Add(new Vector3((float)-3.15, 6, (float)-12.82));
        nextPoints.Add(new Vector3((float)-7.07, 6, (float)-12.09));
        nextPoints.Add(new Vector3((float)-6.78, 6, (float)-9.46));
        nextPoints.Add(new Vector3((float)-7.25, 6, (float)-6.48));
        nextPoints.Add(new Vector3((float)-7, (float) 6.1, (float)-4.12));

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
                if (i != 5) {
                    //Add Next Point
                    addNextPoint(nextPoints[i]);
                }
                else {

                    // StartGame
                    gameStartTrigger.enabled = true;

                }
                
                //To keep the game light we can supress useless LightOrb
                //To implement


            }
        }

	}

    public void moveToPoint(GameObject point) {

        if(pointHistory.Contains(point))
        {
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

    public void addNextPoint(Vector3 v)
    {
        GameObject nextLightOrb = Instantiate(lightOrb, v, Quaternion.identity);
        nextLightOrb.AddComponent<Rigidbody>();
        nextLightOrb.GetComponent<Rigidbody>().useGravity = false;
        nextLightOrb.transform.parent = GameObject.Find("Path").transform;
        

    }

}
