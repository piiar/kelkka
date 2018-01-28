using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHandler : MonoBehaviour {

    public int damage = 10;

    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag("Player")) {
            print("enemy: smash");
            PlayerController enemy = collider.gameObject.GetComponent<PlayerController>();
            enemy.AddDamage(damage, enemy.transform.position - transform.position);

            Rigidbody body = collider.attachedRigidbody;
            if(body == null || body.isKinematic) {
                return;
            }
            Vector3 hitDir = new Vector3(transform.forward.x, 0, transform.forward.z);
            body.velocity = hitDir * 12f;
        }
    }
}
