using UnityEngine;
using UnityEngine.AI;

public class WatcherRobotMovement : MonoBehaviour {
    private Vector3 targetPos;

    private float stoppingRange;

    void Start()
    {
        this.stoppingRange = 20f;
        Stop();
    }

    void Update()
    {
        //Do not navigate if we are on top of our target;
        if (stoppingRange > (Vector3.Distance(targetPos, this.transform.position)));
        {
            NavigateToTarget();
        }
        
    }

    private void NavigateToTarget()
    {
        GetComponent<NavMeshAgent>().SetDestination(targetPos);
    }

    public void Stop()
    {
        targetPos = this.gameObject.transform.position;
    }

    public void SetMovementTarget(Vector3 targetPosition)
    {
        this.targetPos = targetPosition;
    }
}
