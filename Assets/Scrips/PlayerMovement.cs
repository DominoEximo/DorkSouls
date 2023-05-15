using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float StrafeSpeed;
    [SerializeField] private float runSpeed;
    private float dodgeSpeed = 100f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private bool isGrounded;
    float horizontalInput;
    float verticalInput;
    float rotation;
    public CharacterController characterController;
    public Rigidbody rb;
    Vector3 moveDirection;
    Vector3 velocity;
    Vector3 DodgeDistance;

    [SerializeField] private Health hp;
    [SerializeField] public float DelayBeforeInvincible = 0.2f;
    [SerializeField] public float InvincibleDuration = 0.5f;

    [SerializeField] public float DodgeCoolDown = 1;
    private float ActCooldown;
    bool Roll = false;



    [Header("Animation")]
    private Animator animator;


    [Header("Audio")]
    public AudioSource walkOnGrass;
    public AudioSource runOnGrass;

    private void Start()
    {
        hp = GetComponent<Health>();
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        

    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        Roll = Input.GetKey(KeyCode.Space);

        if (ActCooldown <= 0)
        {
            animator.ResetTrigger("Roll");
            if (Roll && isGrounded)
            {
                Dodge();
                
            }
            
        }
        else
        {
            ActCooldown -= Time.deltaTime;
            animator.SetBool("RollForward", false);
            animator.SetBool("RollBackward", false);
            animator.SetBool("RollRight", false);
            animator.SetBool("RollLeft", false);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    #region Move Function
    private void Move()
    {



        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        rotation = Input.GetAxis("Mouse X");

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (isGrounded)
        {


            if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.D))
            {
                runOnGrass.enabled = false;
                walkOnGrass.enabled = true;
                StrafeRight();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.A))
            {
                runOnGrass.enabled = false;
                walkOnGrass.enabled = true;
                StrafeLeft();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.S))
            {
                runOnGrass.enabled = false;
                walkOnGrass.enabled = true;
                WalkBack();
            }
            else if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                runOnGrass.enabled = false;
                walkOnGrass.enabled = true;
                Walk();

            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                walkOnGrass.enabled = false;
                runOnGrass.enabled = true;
                Run();

            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
                walkOnGrass.enabled = false;
                runOnGrass.enabled = false;
            }
            


            moveDirection *= moveSpeed;




            if (rotation != 0)
            {
                RotationAnim();
                
            }
            else
            {
                animator.SetBool("rotatingLeft", false);
                animator.SetBool("rotatingRight", false);
                
            }

        }

        rb.AddForce(moveDirection * Time.fixedDeltaTime, ForceMode.VelocityChange);

        velocity.y += gravity * Time.fixedDeltaTime;

        rb.AddForce(velocity * Time.fixedDeltaTime);


    }
    #endregion

    #region Basic Movement
    private void WalkBack()
    {
        
        moveSpeed = StrafeSpeed;
        animator.SetFloat("Speed", -0.5f, 0.1f, Time.deltaTime);
        animator.SetBool("isStrafing", false);
        animator.SetBool("isIdle", false);

    }
    private void StrafeRight()
    {
        
        moveSpeed = StrafeSpeed;
        animator.SetBool("isStrafing", true);
        animator.SetBool("isIdle", false);
        animator.SetFloat("StrafeSpeed", 0, 0.1f, Time.deltaTime);
    }

    private void StrafeLeft()
    {
        
        moveSpeed = StrafeSpeed;
        animator.SetBool("isStrafing", true);
        animator.SetBool("isIdle", false);

        animator.SetFloat("StrafeSpeed", 1, 0.1f, Time.deltaTime);
    }

    private void Idle()
    {
        animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
        animator.SetBool("isStrafing", false);
        animator.SetBool("isIdle", true);

    }

    private void Walk()
    {
        
        moveSpeed = walkSpeed;
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
        animator.SetBool("isStrafing", false);
        animator.SetBool("isIdle", false);


    }

    private void Run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
        animator.SetBool("isStrafing", false);
        animator.SetBool("isIdle", false);

    }

    private void RotationAnim()
    {



        if (rotation > 0)
        {
            animator.SetBool("rotatingRight", true);
        }
        else if (rotation < 0)
        {
            animator.SetBool("rotatingLeft", true);
        }



    }

    #endregion

    #region Dodging

    void Dodge()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            DodgeDistance = (transform.forward + transform.right) * dodgeSpeed * 10;
            animator.SetBool("RollForward", true);
            animator.SetBool("RollRight", true);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            DodgeDistance = (transform.forward - transform.right) * dodgeSpeed * 10;
            animator.SetBool("RollForward", true);
            animator.SetBool("RollLeft", true);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            DodgeDistance = (-transform.forward + transform.right) * dodgeSpeed * 10;
            animator.SetBool("RollBackward", true);
            animator.SetBool("RollRight", true);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            DodgeDistance = (-transform.forward - transform.right) * dodgeSpeed * 10;
            animator.SetBool("RollBackward", true);
            animator.SetBool("RollLeft", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            DodgeDistance = transform.right * dodgeSpeed * 10;
            animator.SetBool("RollRight", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            DodgeDistance = -transform.right * dodgeSpeed * 10;
            animator.SetBool("RollLeft", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            DodgeDistance = transform.forward * dodgeSpeed * 10;
            animator.SetBool("RollForward", true);
        }
        else
        {
            DodgeDistance = -transform.forward * dodgeSpeed * 10;
            animator.SetBool("RollBackward", true);
        }

        ActCooldown = DodgeCoolDown;
        hp.Invincible(DelayBeforeInvincible, InvincibleDuration);

        //characterController.Move(DodgeDistance * Time.deltaTime);
        rb.AddForce(DodgeDistance * Time.deltaTime, ForceMode.VelocityChange);

        animator.SetTrigger("Roll");
        
    }
    #endregion

}