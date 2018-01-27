using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
    private Vector3 velocity = Vector3.zero;
    private Camera cam;
    public Transform target;
    [Range(5f, 125f)]
    public float distance = 70f;

    void Start() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate() {
        if(target) {
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z - distance);
        }
    }
}
