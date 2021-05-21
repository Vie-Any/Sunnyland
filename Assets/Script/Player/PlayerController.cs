using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * The script to contorl the player
 */
public class PlayerController : MonoBehaviour
{
    //The rigid body object of the player
    public Rigidbody2D rigidbody2D;

    //The collider object of the player's body
    public Collider2D bodyCollider2D;

    // The collider object of the player's head
    public Collider2D headCollider2D;

    //The animator object of the player
    public Animator animator;

    [Space]
    //Movement speed of the player
    public float speed;

    //Jump height of the player
    public float jumpForce;

    [Space]
    //The rigid body object of the ground
    public LayerMask ground;

    [SerializeField]
    // the number of cherry collected by the player
    public int cherry = 0;

    // the text of display component for number of cherry collected by the player
    public Text cherryNum;

    // mark the player is hurt or not
    private bool isHurt = false;

    // mark the player is crouch or not
    private bool isCrouch = false;

    // the audio of jump
    public AudioSource jumpAudio;

    // the audio of hurt
    public AudioSource hurtAudio;

    // the audio of collect cherry
    public AudioSource cherryAudio;

    // the celling point of the player need to check
    public Transform cellingCheck;

    // the check point of the player is land on the ground or not
    public Transform groundCheck;

    // mark the player is land on the ground or not, and mark the player is jump or not
    private bool isGround, isJump;

    // mark the jump button is pressed or not
    private bool jumpPressed;

    // count jump time
    private int jumpCount;

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
        // bodyCollider2D = GetComponent<BoxCollider2D>();
        bodyCollider2D = GetComponent<CircleCollider2D>();
        headCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
        // update the number of cherry collected by the player to the text component text value
        cherryNum.text = cherry.ToString();
    }

    //According to different device performance so that ensure current device could show a good display
    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        if (!isHurt)
        {
            //execute movement every time(only the condition is true will be effected)
            Movement();
            //execute jump every time(only the condition is true will be effected)
            Jump();

            Crouch();
        }

        //execute switch animation every time(only the condition is true will be effected)
        SwitchAnimation();
    }

    //movement function
    void Movement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        // according the direction of movement change the direction of the player face to
        rigidbody2D.velocity = new Vector2(horizontalMove * speed, rigidbody2D.velocity.y);
        
        if (horizontalMove != 0)
        {
            // change the position according to the distance of movement(Time.fixedDeltaTime means the clock rate of the current device)
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
        //the distance of movement
        // float horizontal = Input.GetAxis("Horizontal");
        // //get the direction did the player face to
        // float faceDirection = Input.GetAxisRaw("Horizontal");
        // if (horizontal != 0)
        // {
        //     //change the position according to the distance of movement(Time.fixedDeltaTime means the clock rate of the current device)
        //     rigidbody2D.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, rigidbody2D.velocity.y);
        //     //change the running value of animator's parameter when the function execute every time so that it will show as an animation and switch the player's idle and running state 
        //     animator.SetFloat("running",Mathf.Abs(faceDirection));
        // }
        // //according the direction of movement change the direction of the player face to
        // if (faceDirection != 0)
        // {
        //     transform.localScale = new Vector3(faceDirection, 1, 1);
        // }

        /**
         * implement crouch
         */
        // get the vertical value from input, if the value less than zero then it means the player wants to crouch,
        // so set crouch mark value to true and set crouch animator value to true at the same time
        // if (!isCrouch && Input.GetAxis("Vertical") <0)
        // {
        //     isCrouch = true;
        //     animator.SetBool("crouching",true);
        // }
        // // when the crouch mark is true and the vertical value is not less then zero then it means the player wants to stand up,
        // // so switch the crouch mark value back to false and set crouch animator value to false at the sae time.
        // else if (isCrouch && !(Input.GetAxis("Vertical") <0))
        // {
        //     isCrouch = false;
        //     animator.SetBool("crouching",false);
        // }
        // Crouch();
    }

    //jump function
    void Jump()
    {
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
        //listen to the space(default setting of unity, Unity menu bar>Edit>project setting>Input manager>Axes) button so that to implement jump function
        // if (Input.GetButtonDown("Jump") && bodyCollider2D.IsTouchingLayers(ground))
        // {
        //     rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce * Time.fixedDeltaTime);
        //     jumpAudio.Play();
        //     animator.SetBool("jumping", true);
        // }
    }

    //switch the player's animation
    void SwitchAnimation()
    {
        //for ensure the player will not keep idle state all the time so we need to set idle to false first
        // animator.SetBool("idle", false);
        animator.SetFloat("running", Mathf.Abs(rigidbody2D.velocity.x));
        /**
         * Fix: if the player was fall from platform then the player will not trigger falling animation
         */
        // when the player y speed less than 0.1f and the player was not touching ground then means the player was falling
        // if (rigidbody2D.velocity.y < 0.1f && !bodyCollider2D.IsTouchingLayers(ground))
        // {
        //     animator.SetBool("falling", true);
        // }
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

        if (isHurt)
        {
            // set the parameter of animator hurt value as true
            animator.SetBool("hurt", true);
            // set the running value of animator to zero so that the player will switch animation from running to idle
            animator.SetFloat("running", 0f);
            if (Mathf.Abs(rigidbody2D.velocity.x) < 0.1)
            {
                // turn back the parameter of animator hurt value to false
                animator.SetBool("hurt", false);
                // mean while set the parameter idle as true
                animator.SetBool("idle", true);
                // and set hurt mark value back to false
                isHurt = false;
            }
        }

        //when the player is jumping state
        // if (animator.GetBool("jumping"))
        // {
        //     //if the player's y less than 0 that means the player was falling
        //     if (rigidbody2D.velocity.y < 0)
        //     {
        //         //change jumping to false
        //         animator.SetBool("jumping", false);
        //         //mean while set falling as true
        //         animator.SetBool("falling", true);
        //     }
        // }
        // // if the player was hurt then change the hurt mark according the movement speed
        // else if (isHurt)
        // {
        //     // set the parameter of animator hurt value as true
        //     animator.SetBool("hurt", true);
        //     // set the running value of animator to zero so that the player will switch animation from running to idle
        //     animator.SetFloat("running", 0f);
        //     // when the player was hurt and the movement speed is less than 0.1 then change the hurt mark back to false
        //     if (Mathf.Abs(rigidbody2D.velocity.x) < 0.1)
        //     {
        //         // turn back the parameter of animator hurt value to false
        //         animator.SetBool("hurt", false);
        //         // mean while set the parameter idle as true
        //         animator.SetBool("idle", true);
        //         // and set hurt mark value back to false
        //         isHurt = false;
        //     }
        // }
        // //when the player touching ground then it means the player was on the ground
        // else if (bodyCollider2D.IsTouchingLayers(ground))
        // {
        //     //change falling to false
        //     animator.SetBool("falling", false);
        //     //mean while set idle as true
        //     animator.SetBool("idle", true);
        // }
    }

    // if another collider touching current collider then trigger the function
    private void OnTriggerEnter2D(Collider2D other)
    {
        // collect goods
        if (other.CompareTag("Collection"))
        {
            cherryAudio.Play();
            // Destroy(other.gameObject);
            // cherry += 1;
            other.GetComponent<Animator>().Play("GoodsBoom");
            other.GetComponent<Collider2D>().enabled = false;
        }

        // the player drop out of the scene then restart current scene
        if (other.tag == "DeadLine")
        {
            // disable all the audio of the scene
            GetComponent<AudioSource>().enabled = false;
            // delay 2 seconds then restart current scene
            Invoke("Restart", 2f);
        }
    }

    // destory the enemy
    private void OnCollisionEnter2D(Collision2D other)
    {
        // if the touched object tag is enemy then go to the next judge statement
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            // if the player is falling then destory the enemy
            if (animator.GetBool("falling"))
            {
                enemy.getHit();
                // let the player jump again.
                // rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce * Time.fixedDeltaTime);
                // animator.SetBool("jumping", true);
                jumpPressed = true;
                jumpCount++;
                isJump = true;
            }
            else if (transform.position.x < other.gameObject.transform.position.x)
            {
                rigidbody2D.velocity = new Vector2(-10, rigidbody2D.velocity.y);
                hurtAudio.Play();
                isHurt = true;
            }
            else if (transform.position.x > other.gameObject.transform.position.x)
            {
                rigidbody2D.velocity = new Vector2(10, rigidbody2D.velocity.y);
                hurtAudio.Play();
                isHurt = true;
            }
        }
    }

    // crouch function
    void Crouch()
    {
        // condition: the player must land on the ground and the celling check point not touch the ground
        if (isGround && !Physics2D.OverlapCircle(cellingCheck.position,0.2f,ground))
        {
            if (Input.GetButton("Crouch"))
            {
                animator.SetBool("crouching", true);
                headCollider2D.enabled = false;
                isCrouch = true;
            }
            else
            {
                animator.SetBool("crouching", false);
                headCollider2D.enabled = true;
                isCrouch = false;
            }
            // when the Crouch key down set the crouching state true for trigger crouch animaiont play
            // if (Input.GetButton("Crouch"))
            // {
            //     animator.SetBool("crouching", true);
            //     headCollider2D.enabled = false;
            //     isCrouch = true;
            // }
            // // when the Crouch key up set the crouching state false
            // else
            // {
            //     animator.SetBool("crouching", false);
            //     headCollider2D.enabled = true;
            //     isCrouch = false;
            // }
        }
    }

    // restart current scene
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore()
    {
        cherry++;
    }
}