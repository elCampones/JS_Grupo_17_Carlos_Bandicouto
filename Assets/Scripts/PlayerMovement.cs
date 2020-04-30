using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(PlayerInput))]
[RequireComponent (typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    private Rigidbody rb;

    private Animator animator;

    [SerializeField]
    private float jumpForce = 100f;

    [SerializeField]
    private float doubleJumpForce = 75f;

    [SerializeField]
    private float speedGrounded = 10f;

    [SerializeField]
    private float speedAir = 6f;

    [SerializeField]
    private float rotationTime = .2f;

    [SerializeField]
    private float speedTime = .1f;

    private float targetRotation;

    private float currentAngularSpeed;

    private float targetSpeed;

    private float currentSpeed;

    private bool isGrounded = false;

    private bool hasDoubleJumped = false;

    Transform cameraTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentAngularSpeed = transform.rotation.y;
        cameraTransform = GetComponent<PlayerInput>().camera.transform;
    }

    private void Update()
    {
        float frameSpeed = Mathf.SmoothDamp(new Vector2(rb.velocity.x, rb.velocity.z).magnitude, targetSpeed, ref currentSpeed, speedTime);
        animator.SetFloat("SpeedRatio", frameSpeed / speedGrounded);
        rb.velocity = (transform.forward * frameSpeed) + Vector3.up * rb.velocity.y;
    }

    private void FixedUpdate()
    {
        if (targetSpeed != 0f)
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref currentAngularSpeed, rotationTime);                                                                                                                                               
        }

    public void Move (CallbackContext value)
    {
        if (value.canceled)
            targetSpeed = 0f;
        else
            MoveTowards(value.ReadValue<Vector2>());
    }

    private void MoveTowards(Vector2 directionAlongXZPlane)
    {
        targetRotation = (Mathf.Atan2(directionAlongXZPlane.x, directionAlongXZPlane.y) + Mathf.Atan2(cameraTransform.forward.x, cameraTransform.forward.z)) * Mathf.Rad2Deg;
        targetSpeed = isGrounded ? speedGrounded : speedAir;
    }    


    public void Jump(CallbackContext value)
    {
        if (value.performed)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce);
                animator.SetTrigger("jumpTrigger");
            }
            else if (!hasDoubleJumped)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * doubleJumpForce);
                hasDoubleJumped = true;
                animator.SetTrigger("jumpTrigger");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            isGrounded = true;
            //this.enteringGround(true);
            hasDoubleJumped = false;
            if (targetSpeed == speedAir)
                targetSpeed = speedGrounded;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            //this.enteringGround(false);
            isGrounded = false;
        }
    }

    private void enteringGround(bool isEntering)
    {
        isGrounded = isEntering;
        animator.SetBool("isRunning", isEntering);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Restart")
            transform.position = Vector3.zero;
    }

}
