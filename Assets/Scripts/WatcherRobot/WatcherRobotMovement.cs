using UnityEngine;
using UnityEngine.AI;

public enum AiMode {
    Aggressive,
    Flanking,
    Objective
}

public class WatcherRobotMovement : MonoBehaviour {
    private static float accurateNavigationRange = 8.0f;
    private static float stoppingRange = 2.0f;
    private static float thinkCounterMax = 2.0f; // Seconds

    private static float flankRadius = 4.0f;

    public Transform target;
    private float thinkCounter;
    private NavMeshAgent agent;
    //private AiMode aiMode = AiMode.Flank;

    private int flankDirection = 1; // or -1. Can be randomized

    void Start() {
        thinkCounter = Random.Range(0, thinkCounterMax);
        agent = GetComponent<NavMeshAgent>();
        this.target = GameObject.FindGameObjectWithTag("Player").transform;
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

        // Invoke the currently selected ai routine
        if (FindAiType() == AiMode.Aggressive) {
            modeAggressive(distanceToTarget);
        }
        else if (FindAiType() == AiMode.Flanking) {
            modeFlank(distanceToTarget);
        }
        else if(FindAiType() == AiMode.Aggressive) {
            modeAggressive(distanceToTarget);
        }
    }

    // Note that this mode lacks all the nice optimizations in aggressive
    private void modeFlank(float distanceToTarget) {
        Vector3 flankPoint = calculateNewFlankPoint();

        navigateTo(flankPoint);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        var p = calculateNewFlankPoint();
        //p = new Vector3(p.x, p.z, 0);
        Gizmos.DrawSphere(p, 0.5f);
    }

    // Calculates a new flank point such that the point is at a 30 deg
    // angle relative to the current angle between the objects, and at
    // the specified distance.
    // The 30 deg is not a "must". You can try others.
    private Vector3 calculateNewFlankPoint() {
        Vector3 center = target.position;
        
        Vector2 a = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 b = new Vector2(target.position.x, target.position.z);
        a = b - a;
        b = Vector2.zero;
        float angle = -(Mathf.Atan2(b.y, b.x) - Mathf.Atan2(a.y, a.x)) * Mathf.Rad2Deg + 180;
        angle += flankDirection * 30;

        Vector3 flankPoint = new Vector3(
            center.x + Mathf.Cos(angle * Mathf.Deg2Rad) * flankRadius,
            center.y,
            center.z + Mathf.Sin(angle * Mathf.Deg2Rad) * flankRadius
        );

        return flankPoint;
    }

    private void modeAggressive(float distanceToTarget) {
        Vector3 targetPos = target.position;

        if (distanceToTarget < stoppingRange) {
            // Do not move if close enough
            agent.isStopped = true;
            thinkCounter = Random.Range(0, thinkCounterMax);
        }
        else if (distanceToTarget < accurateNavigationRange) {
            // Midrange, update target every frame
            navigateTo(targetPos);
        }
        else {
            // Far away
            navigateTo(targetPos);
            thinkCounter = thinkCounterMax;
        }
    }

    private void navigateTo(Vector3 pos) {
        agent.isStopped = false;
        agent.SetDestination(pos);
    }

    public void Stop() {
        agent.isStopped = true;
        target = null;
    }

    public void SetMovementTarget(Transform target) {
        this.target = target;
    }

    private AiMode FindAiType()
    {
        return GetComponent<EnemyController>().GetAIMode();
    }

    private float GetMovespeedEquipmentModifier()
    {
        return GetComponent<EnemyController>().MoveSpeed();
    }
}
