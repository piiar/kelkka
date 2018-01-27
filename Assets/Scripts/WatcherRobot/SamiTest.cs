using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamiTest : MonoBehaviour {
    public GameObject miniRobot;
    public GameObject targetObject;
    public WatcherRobotMovement navigation;

    public void Start() {
        navigation = miniRobot.GetComponent<WatcherRobotMovement>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            navigation.SetMovementTarget(targetObject.transform);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            navigation.Stop();
        }

    }
}
