using UnityEngine;

public class PlayerController : MonoBehaviour {

    private string userId = null;
    private Vector3 movement = Vector3.zero;
    private float lookAngle = 0f;
    private CharacterController controller;

    [Range(5f, 15f)]public float speed = 5f;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
    }

	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 direction = new Vector3(h, 0f, v);
        if(direction != Vector3.zero) {
            direction.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }

        //lookAngle += mouseX * 2f;
        //transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);

        movement = direction * Time.deltaTime * speed;
    }

    void FixedUpdate() {
        controller.Move(movement);
    }
}