using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    // the rigid body of the frog
    private Rigidbody2D rigidbody2D;

    // the aminator of the frog
    private Animator animator;

    // the collider of the frog
    private Collider2D collider2D;

    //The rigid body object of the ground
    public LayerMask ground;

    // the margin point of the frog counld movement
    public Transform leftPoint, rightPoint;

    [SerializeField]
    // store the margin point of the frog allow to movement value
    private float leftPointX, rightPointX;

    // the frog's movement speed
    public float speed;

    // the distance did the frog could jump
    public float jumpForce;

    // define the direction of the frog faced to and make the default value true instead of the frog face to left direction
    private bool FaceLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<CircleCollider2D>();
        leftPointX = leftPoint.position.x;
        rightPointX = rightPoint.position.x;
        // destory the margin point object
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
        SwichAnimation();
    }

    // movement function of the frog
    public void Movement()
    {
        if (FaceLeft)
        {
            // when the frog position x less than left margin point then reverse the frog's movement direction
            // and the picture of the frog so that the result looks like the frog was reverse the direction
            if (transform.position.x < leftPointX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                FaceLeft = false;
            }

            Debug.Log("69：FaceLeft: " + FaceLeft + ", player X: " + transform.position.x + ", leftPointX: " + leftPointX);
        }
        else
        {
            // when the frog position x great than right margin point then reverse the frog's movement direction
            // and the picture of the frog so that the result looks like the frog was reverse the direction
            if (transform.position.x > rightPointX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                FaceLeft = true;
            }
        }
        Jump();
    }

    // jump function
    private void Jump()
    {
        if (FaceLeft && collider2D.IsTouchingLayers(ground))
        {
            animator.SetBool("jumping", true);
            // change the position of the frog so that the frog looks like moving
            rigidbody2D.velocity = new Vector2(-speed, jumpForce);
        }else if (!FaceLeft && collider2D.IsTouchingLayers(ground))
        {
            animator.SetBool("jumping", true);
            // change the position of the frog so that the frog looks like moving
            rigidbody2D.velocity = new Vector2(speed, jumpForce);
        }
    }

    // switch animation function
    public void SwichAnimation()
    {
        // logic for control the frog jumping animation state swich
        if (animator.GetBool("jumping"))
        {
            // when the frog falling speed less than 0.1 then it means needs to change the animation jumping to false
            // and mean while set the animation falling true
            if (rigidbody2D.velocity.y < 0.1f)
            {
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
            }
        }

        // when the frog is touching the ground and the animation falling state is true then change the animation falling back to false
        if (collider2D.IsTouchingLayers(ground) && animator.GetBool("falling"))
        {
            animator.SetBool("falling", false);
        }
    }

    // the frog was hitted by player
    public void getHit()
    {
        animator.SetTrigger("death");
    }

    public void death()
    {
        Destroy(gameObject);
    }
}