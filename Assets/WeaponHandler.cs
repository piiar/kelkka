using UnityEngine;

public class WeaponHandler : MonoBehaviour {

    public SkinnedMeshRenderer skinnedMeshRenderer;
    public TrailRenderer trailRenderer;
    public int damage = 10;

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
            EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();
            enemy.AddDamage(damage, enemy.transform.position - transform.position);
        }
    }

    public void Extend() {
        trailRenderer.enabled = true;
        skinnedMeshRenderer.SetBlendShapeWeight(0, 0f);
        //if(blendState > 0f) {
        //    skinnedMeshRenderer.SetBlendShapeWeight(0, blendState);
        //    blendState -= 1f;
        //}
    }

    public void Shrink() {
        trailRenderer.enabled = false;
        skinnedMeshRenderer.SetBlendShapeWeight(0, 100f);
    }
}
