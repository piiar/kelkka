using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

    private int value; 

	// Use this for initialization
	void Start () {
        this.value = 20;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggering: " + other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            //Give Player something good :) 
            //other.GetComponent<PointController>()
            Destroy(this.gameObject);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "player")
        {
            //Give Player something good :) 
            //other.GetComponent<PointController>()
            Destroy(this.gameObject);
        }
    }*/
}
