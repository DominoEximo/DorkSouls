using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeRoll : MonoBehaviour    
{

    private Health hp;
    private Rigidbody rb;

    private Animator animator;

    public float DelayBeforeInvincible = 0.2f;
    public float InvincibleDuration = 0.5f;

    public float DodgeCoolDown = 1;
    private float ActCooldown;

    public float PushAmt = 3;

    private Vector3 moveDirection;
    float horizontalInput;
    float verticalInput;
    public CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Health>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        bool Roll = Input.GetKey(KeyCode.LeftAlt);


        if (ActCooldown <= 0)
        {
            animator.ResetTrigger("Roll");
            if(Roll)
            {
                Dodge();
            }
        }
        else
        {
            ActCooldown -= Time.deltaTime;
        }


        void Dodge()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            

            moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
            ActCooldown = DodgeCoolDown;
            hp.Invincible(DelayBeforeInvincible, InvincibleDuration);

            characterController.Move(moveDirection * Time.deltaTime * 20);


            animator.SetTrigger("Roll");
        }
    }
}
