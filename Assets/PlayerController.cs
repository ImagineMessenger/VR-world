using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int speed;

    public Vector3 nextPosition;
    public bool isMoving;
    private List<GameObject> pointHistory;



    // Use this for initialization
    void Start () {

        isMoving = false;
        pointHistory = new List<GameObject>();

    }
	
	// Update is called once per frame
	void Update () {

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
            if(this.transform.position == nextPosition)
            {
                isMoving = false;
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

}
