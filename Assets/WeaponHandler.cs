using UnityEngine;

public class WeaponHandler : MonoBehaviour {

    public SkinnedMeshRenderer skinnedMeshRenderer;
    public TrailRenderer trailRenderer;
    public Collider col;
    public int damage = 10;
    float timeStamp;

    void Awake() {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        col = GetComponent<Collider>();
        timeStamp = Time.time + 0.3f;
    }

	// Update is called once per frame
	void OnTriggerEnter (Collider collider) {
        if(collider.CompareTag("Obstacle")) {
            Rigidbody body = collider.attachedRigidbody;
            if(body == null || body.isKinematic) {
                return;
            }
            Vector3 hitDir = new Vector3(transform.forward.x, 0, transform.forward.z);
            body.velocity = hitDir * 12f;
        }

        if(collider.CompareTag("Enemy")) {
            print("player: smash");
            EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();
            enemy.AddDamage(10, enemy.transform.position - transform.position);
        }
    }

    public void Extend() {
        col.enabled = true;
        //trailRenderer.enabled = true;
        skinnedMeshRenderer.SetBlendShapeWeight(0, 0f);
    }

    public void Shrink() {
        col.enabled = false;
        trailRenderer.enabled = false;
        skinnedMeshRenderer.SetBlendShapeWeight(0, 100f);
    }
}
