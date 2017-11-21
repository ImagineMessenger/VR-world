using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        

    }

    public void fallingToGround(){
        if (this.GetComponent<Rigidbody>())
        {
            this.GetComponent<Rigidbody>().useGravity = true;
        
        } 
    }

    void OnCollisionEnter(Collision col)
    {
        if (this.GetComponent<Rigidbody>()) {
            Destroy(this.GetComponent<Rigidbody>());
            
        }
        

    }
}
