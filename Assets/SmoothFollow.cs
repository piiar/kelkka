using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
    private Vector3 velocity = Vector3.zero;
    private Camera cam;
    public Transform target;
 
    void Start() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate() {
        if(target) {
            float y = transform.position.y;
            transform.position = new Vector3(target.position.x, y, target.position.z);
        }
    }
}
