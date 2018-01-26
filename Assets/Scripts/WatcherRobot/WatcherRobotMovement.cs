using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WatcherRobotMovement : MonoBehaviour {
    public GameObject target;

    public void NavigateToTarget(){
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            NavigateToTarget();
        }
    }
}
