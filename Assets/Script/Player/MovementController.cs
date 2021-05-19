using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Collider2D headCollider2D;
    private Collider2D bodyCollider2D;
    private Animator animator;

    public float speed, jumpForce;

    public Transform groundCheck;

    public LayerMask ground;

    public bool isGround, isJump;

    private bool jumpPressed;

    private int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        headCollider2D = GetComponent<BoxCollider2D>();
        bodyCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
        GroundMovement();
        Jump();
        SwitchAnimation();
    }

    void GroundMovement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(horizontalMove * speed, rigidbody2D.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    void Jump()
    {
        Debug.Log("Jump: "+(Input.GetButtonDown("Jump"))+" Horizontal: "+ (Input.GetAxisRaw("Horizontal")));
        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
        }

        if (jumpPressed && isGround)
        {
            isJump = true;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }

    void SwitchAnimation()
    {
        if (Mathf.Abs(rigidbody2D.velocity.x) > 0.1f)
        {
            Debug.Log("running: " + (Mathf.Abs(rigidbody2D.velocity.x)));
        }
        animator.SetFloat("running", Mathf.Abs(rigidbody2D.velocity.x));
        if (isGround)
        {
            animator.SetBool("falling", false);
        }
        else if (!isGround && rigidbody2D.velocity.y > 0)
        {
            animator.SetBool("jumping",true);
        }
        else if (rigidbody2D.velocity.y < 0)
        {
            animator.SetBool("jumping",false);
            animator.SetBool("falling",true);
        }
    }
}