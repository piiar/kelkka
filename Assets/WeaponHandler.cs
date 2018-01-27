using UnityEngine;

public class WeaponHandler : MonoBehaviour {

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
}
