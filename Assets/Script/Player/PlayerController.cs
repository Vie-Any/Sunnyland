using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * The script to contorl the player
 */
public class PlayerController : MonoBehaviour
{
    //The rigid body object of the player
    public Rigidbody2D rigidbody2D;

    //The collider object of the player
    public Collider2D collider2D;

    //The animator object of the player
    public Animator animator;

    //Movement speed of the player
    public float speed;

    //Jump height of the player
    public float jumpForce;

    //The rigid body object of the ground
    public LayerMask ground;

    // the number of cherry collected by the player
    public int cherry = 0;

    // the text of display component for number of cherry collected by the player
    public Text cherryNum;

    // Start is called before the first frame update
    void Start()
    {
        //Init rigid body object
        rigidbody2D = GetComponent<Rigidbody2D>();
        //Init animator object
        animator = GetComponent<Animator>();
        //Init collider object
        /**
         * Bug fix , BoxCollider will cause a bug[refer to the DailyLog.md file at 2021-05-07], it's because BoxCollider's corner will touching ground's unit corner, then it will make the player cannot move.
         * To fix the bug: use two collider, BoxCollider is for the player's upper body, the CircleCollider for the player's lower body
         */
        // collider2D = GetComponent<BoxCollider2D>();
        collider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    //According to different device performance so that ensure current device could show a good display
    void FixedUpdate()
    {
        //execute movement every time(only the condition is true will be effected)
        Movement();
        //execute jump every time(only the condition is true will be effected)
        Jump();
        //execute switch animation every time(only the condition is true will be effected)
        SwitchAnimation();
    }

    //movement function
    void Movement()
    {
        //the distance of movement
        float horizontal = Input.GetAxis("Horizontal");
        //get the direction did the player face to
        float faceDirection = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            //change the position according to the distance of movement(Time.deltaTime means the clock rate of the current device)
            rigidbody2D.velocity = new Vector2(horizontal * speed * Time.deltaTime, rigidbody2D.velocity.y);
            //change the running value of animator's parameter when the function execute every time so that it will show as an animation and switch the player's idle and running state 
            animator.SetFloat("running",Mathf.Abs(faceDirection));
        }
        //according the direction of movement change the direction of the player face to
        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
        
    }

    //jump function
    void Jump()
    {
        //listen to the space(default setting of unity, Unity menu bar>Edit>project setting>Input manager>Axes) button so that to implement jump function
        if (Input.GetButtonDown("Jump") && collider2D.IsTouchingLayers(ground))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce * Time.deltaTime);
            animator.SetBool("jumping",true);
        }
    }
    
    //switch the player's animation
    void SwitchAnimation()
    {
        //for ensure the player will not keep idle state all the time so we need to set idle to false first
        animator.SetBool("idle",false);
        //when the player is jumping state
        if (animator.GetBool("jumping"))
        {
            //if the player's y less than 0 that means the player was falling
            if (rigidbody2D.velocity.y < 0)
            {
                //change jumping to false
                animator.SetBool("jumping",false);
                //mean while set falling as true
                animator.SetBool("falling",true);
            }
        }
        //when the player touching ground then it means the player was on the ground
        else if (collider2D.IsTouchingLayers(ground))
        {
            //change falling to false
            animator.SetBool("falling",false);
            //mean while set idle as true
            animator.SetBool("idle",true );
        }
    }

    // if another collider touching current collider then trigger the function
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collection"))
        {
            Destroy(other.gameObject);
            Debug.Log("before cherry:"+cherry);
            cherry += 1;
            Debug.Log("after cherry:"+cherry);
            // update the number of cherry collected by the player to the text component text value
            cherryNum.text = cherry.ToString();
        }
    }
}