using UnityEngine;

public class PlayerController : MonoBehaviour {

    private string userId = null;
    private Animator anim;
    private readonly int legHash = Animator.StringToHash("Leg");
    private readonly int attackHash = Animator.StringToHash("AttackMelee");
    private readonly int blockHash = Animator.StringToHash("Block");
    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int finishedHash = Animator.StringToHash("AnimationFinished");
    private bool isAttacking;
    private bool isBlocking;
    private Vector3 moveDirection = Vector3.zero;

    private Vector3 movement = Vector3.zero;
    private float lookAngle = 0f;
    private CharacterController controller;
    public ParticleSystem dustEmitter;
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
    float verticalSpeed = 0f;
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

        moveDirection = new Vector3(h, 0f, v);
        if(moveDirection != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }

        //lookAngle += mouseX * 2f;
        //transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);
        verticalSpeed -= 9.82f * Time.deltaTime;

        velocity = Mathf.Min(moveDirection.sqrMagnitude, 1f) * moveSpeed;
        movement = moveDirection * Time.deltaTime * velocity + new Vector3(0, verticalSpeed, 0);
    }

    void FixedUpdate() {
        controller.Move(movement);
        UpdateAnimator();
    }

    void UpdateAnimator() {
        //bool finishedLastAnimation = anim.GetBool(finishedHash);
        if(isAttacking) {
            anim.SetTrigger(attackHash);
            isAttacking = false;
        }
        else if(isBlocking) {
            anim.SetTrigger(blockHash);
            isBlocking = false;
        }

        anim.SetFloat(speedHash, velocity, 0.1f, Time.deltaTime);

        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(1);
        if(state.IsName("AttackMelee") && state.normalizedTime >= 1f) {
            SetWeaponColliderActive(false);
        }
    }

    private float feetHit;
    private float lastFeetHit;
    void OnControllerColliderHit(ControllerColliderHit hit) {
        float runCycle = Mathf.Repeat(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f);
        float feetHit = (runCycle < 0.5f ? 1 : -1);
        print(feetHit);
        float speed = anim.GetFloat(speedHash);

        if(feetHit != lastFeetHit) {
            if(speed > 9f) {
                dustEmitter.Emit(1);
            }  
        }
        lastFeetHit = feetHit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if(body == null || body.isKinematic)
            return;

        if(hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * 2f;
    }

    void SetWeaponColliderActive(bool enabled) {
        weaponCollider.gameObject.SetActive(enabled);// = enabled;
    }
}