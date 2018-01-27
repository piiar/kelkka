using UnityEngine;

public class PlayerController : MonoBehaviour {

    private string userId = null;
    private Animator anim;
    private int attackHash = Animator.StringToHash("AttackMelee");
    private int blockHash = Animator.StringToHash("Block");
    private int speedHash = Animator.StringToHash("Speed");
    private int finishedHash = Animator.StringToHash("AnimationFinished");
    private bool isAttacking;
    private bool isBlocking;

    private Vector3 movement = Vector3.zero;
    private float lookAngle = 0f;
    private CharacterController controller;
    public Collider weaponCollider;

    [Range(5f, 25f)]public float moveSpeed = 15f;
    private float velocity = 0f;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        weaponCollider.enabled = false;
    }

	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        
        bool jumpBtn = Input.GetButtonDown("Jump");
        isAttacking = Input.GetButtonDown("Fire1");
        isBlocking = Input.GetButtonDown("Fire3");

        if(isAttacking) {
            SetWeaponColliderActive(true);
        }

        Vector3 direction = new Vector3(h, 0f, v);
        if(direction != Vector3.zero) {
            direction.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }

        //lookAngle += mouseX * 2f;
        //transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);
        velocity = Mathf.Min(direction.sqrMagnitude, 1f) * moveSpeed;
        movement = direction * Time.deltaTime * velocity;
    }

    void FixedUpdate() {
        controller.Move(movement);
        UpdateAnimator();
    }

    void UpdateAnimator() {
        bool finishedLastAnimation = anim.GetBool(finishedHash);

        //if(finishedLastAnimation) {
            if(isAttacking) {
                anim.SetTrigger(attackHash);
                isAttacking = false;
            }
            else if(isBlocking) {
                anim.SetTrigger(blockHash);
                isBlocking = false;
            }
        //}

        anim.SetFloat(speedHash, velocity, 0.1f, Time.deltaTime);

        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(1);
        if(state.IsName("AttackMelee") && state.normalizedTime >= 1f) {
            SetWeaponColliderActive(false);
        }
    }

    void SetWeaponColliderActive(bool enabled) {
        weaponCollider.gameObject.SetActive(enabled);// = enabled;
        //anim.SetBool(finishedHash, false);
    }
}