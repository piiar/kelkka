using UnityEngine;
using UnityEngine.AI;

public class WatcherRobotMovement : MonoBehaviour {
    private static float accurateNavigationRange = 8.0f;
    private static float stoppingRange = 2.0f;
    private static float thinkCounterMax = 2.0f; // Seconds

    private Transform target;
    private float thinkCounter;
    private NavMeshAgent agent;

    void Start() {
        thinkCounter = Random.Range(0, thinkCounterMax);
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (target == null) {
            return;
        }

        // Try to save bit of processing power, and update the target more rarely.
        // Also creates some divergence to the behaviour of robots.
        thinkCounter -= Time.deltaTime;
        if (thinkCounter > 0) {
            return;
        }

        // Strip away height differences to get accurate distance
        Vector2 a = new Vector2(target.position.x, target.position.z);
        Vector2 b = new Vector2(this.transform.position.x, this.transform.position.z);

        float distanceToTarget = Vector2.Distance(a, b);
        if (distanceToTarget < stoppingRange) {
            // Do not move if close enough
            agent.isStopped = true;
            thinkCounter = Random.Range(0, thinkCounterMax);
        }
        else if (distanceToTarget < accurateNavigationRange) {
            // Midrange, update target every frame
            NavigateToTarget();
        }
        else {
            // Far away
            NavigateToTarget();
            thinkCounter = thinkCounterMax;
        }

    }

    private void NavigateToTarget() {
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }

    public void Stop() {
        agent.isStopped = true;
        target = null;
    }

    public void SetMovementTarget(Transform target) {
        this.target = target;
    }
}
