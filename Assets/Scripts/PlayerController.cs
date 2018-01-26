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

        float mouseX = Input.GetAxis("Mouse X"); // + Input.GetAxis("Horizontal") * 10f;
        float mouseY = Input.GetAxis("Mouse Y"); // + Input.GetAxis("Vertical") * -8f;

        Vector3 direction = new Vector3(h, transform.position.y, v);
        if(direction != Vector3.zero) {
            direction.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }
        movement = direction * Time.deltaTime * 5f;
    }

    void FixedUpdate() {
        controller.Move(movement);
    }


    //public void HandleRotationMovement(Vector3 input) {
    //    float lookAngle += input.x * 2f;
    //    transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);
    //}
}
