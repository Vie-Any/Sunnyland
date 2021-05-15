using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    // the rigid body of the eagle
    private Rigidbody2D rigidbody2D;

    // the margin point of the eagle counld movement
    public Transform top, bottom;

    [SerializeField]
    // store the margin point of the frog allow to movement value
    private float topY, bottomY;

    // the eagle's movement speed
    public float speed;

    // mark the eagle going up or not
    private bool goingUp = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rigidbody2D = GetComponent<Rigidbody2D>();
        topY = top.position.y;
        bottomY = bottom.position.y;
        // destory the margin point object
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Fly();
        }
    }

    // movement function of the eagle
    public void Fly()
    {
        SwichMovementDirection();
        if (goingUp)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.position.x, speed);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.position.x, -speed);
        }
    }

    // change the mark of the eagle is going up or not
    public void SwichMovementDirection()
    {
        if (goingUp && transform.position.y > topY)
        {
            goingUp = false;
        }
        else if (!goingUp && transform.position.y < bottomY)
        {
            goingUp = true;
        }
    }
}