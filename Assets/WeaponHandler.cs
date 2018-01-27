using UnityEngine;

public class WeaponHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
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
	}
}
