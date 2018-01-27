using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamiTest : MonoBehaviour {
    public GameObject miniRobot;

    public GameObject targetObject;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            miniRobot.GetComponent<WatcherRobotMovement>().SetMovementTarget(targetObject.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            miniRobot.GetComponent<WatcherRobotMovement>().Stop();
        }

    }
}
