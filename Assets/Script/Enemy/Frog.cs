using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    // the rigid body of the frog
    private Rigidbody2D rigidbody2D;
    
    // the margin point of the frog counld movement
    public Transform leftPoint, rightPoint;
    // store the margin point of the frog allow to movement value
    private float leftPointX, rightPointX;

    // the frog's movement speed
    public float speed ;

    // define the direction of the frog faced to and make the default value true instead of the frog face to left direction
    private bool FaceLeft = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        leftPointX = leftPoint.position.x;
        rightPointX = rightPoint.position.x;
        // destory the margin point object
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    // movement function of the frog
    public void Movement()
    {
        if (FaceLeft)
        {
            // change the position of the frog so that the frog looks like moving
            rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
            // when the frog position x less than left margin point then reverse the frog's movement direction
            // and the picture of the frog so that the result looks like the frog was reverse the direction
            if (transform.position.x < leftPointX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                FaceLeft = false;
            }
        }
        else
        {
            // change the position of the frog so that the frog looks like moving
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
            // when the frog position x great than right margin point then reverse the frog's movement direction
            // and the picture of the frog so that the result looks like the frog was reverse the direction
            if (transform.position.x > rightPointX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                FaceLeft = true;
            } 
        }
    }
}
