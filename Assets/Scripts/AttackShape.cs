using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackShape : MonoBehaviour {
    public GameObject attackHandler;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        attackHandler.GetComponent<AttackHandler>().OnDamage();   
    }
}
