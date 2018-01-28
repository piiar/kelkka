using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour {
    public GameObject attackCollider;

    public float attackRate;
    private float nextAttack;

    public float attackColliderEnabledTimer;

    public bool attackColliderEnabled;
	// Use this for initialization
	void Start () {
        attackCollider.SetActive(false);	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0) && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            
            attackCollider.SetActive(true);
            attackColliderEnabled = true;
        }

        if (attackColliderEnabled == true)
        {
            attackColliderEnabledTimer++;
            if (attackColliderEnabledTimer > 10)
            {
                attackColliderEnabledTimer = 0;
                attackColliderEnabled = false;
                attackCollider.SetActive(false);
            }
        }
	}

    public void OnDamage()
    {
        Debug.Log("DealtDamage, Hell Yeah!");
    }
}
