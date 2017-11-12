using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour {

    public GameObject lightOrb;

    // Use this for initialization
    void Start () {

        
    }

    void OnCollisionEnter(Collision collision)
    {
        lightOrb.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
