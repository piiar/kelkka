using UnityEngine;

public class PlayerController : MonoBehaviour {

    private string userId = null;
    private Vector3 movement = Vector3.zero;
    private CharacterController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
    }

	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0f, v);
        if(direction != Vector3.zero) {
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }
        movement = direction * Time.deltaTime * 5f;
    }

    void FixedUpdate() {
        controller.Move(movement);
    }
}
