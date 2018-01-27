using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WatcherRobotMovement))]
public class SetNavigationTarget : MonoBehaviour {
    public GameObject Target;

	// Use this for initialization
	void Start () {
        GetComponent<WatcherRobotMovement>().SetMovementTarget(Target.transform);
	}
}
