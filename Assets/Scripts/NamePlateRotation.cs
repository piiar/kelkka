using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamePlateRotation : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        Transform lookAtPos = Camera.main.transform;
        transform.LookAt(2 * transform.position - lookAtPos.position);
    }
}
