using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    public Transform wallGrabPoint;
    public Transform groundCheck;

    public LayerMask whatIsGround;
    public CharacterController2D controller;
    public static float runSpeed = 7.5f;
    float horizontalMove = 0f;
    public Rigidbody2D theRB;
    public float jumpForce = 10f;

    private bool isGrounded;
    [SerializeField]
    //private bool canGrab, isGrabbing;

    private bool jump = false;
    private float gravityStore;

    public Animator animator;
    public Rigidbody2D rb;

    private void Start()
    {
        gravityStore = theRB.gravityScale;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        //handle wall jumping
        //canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, 0.2f, whatIsGround);
        //isGrabbing = false;
        //if (canGrab)
        //{
        //    if(transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") > 0 || (transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") < 0))
        //    {
        //        isGrabbing = true;
        //    }
        //}

        //if(isGrabbing)
        //{
        //    theRB.velocity = Vector2.up * jumpForce;
        //}
        //else
        //{
        //    theRB.gravityScale = gravityStore;  
        //}
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        //Move our character
        controller.Move(horizontalMove, false, jump);
        jump = false;
    }
}
