using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //人物的刚体对象
    public Rigidbody2D rigidbody2D;

    //人物的移动速度
    public float speed;

    //跳跃的距离
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    //固定每秒60帧的刷新执行频率
    // Update is called once per frame
    //void Update()
    //{
    //}

    //根据不同设备的情况保证不同设备能够以当前设备的能力变更每一帧的画面
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        //移动距离
        float horizontal = Input.GetAxis("Horizontal");
        //面对的方向
        float faceDirection = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            //变更移动距离(Time.deltaTime表示设备的时钟频率)
            rigidbody2D.velocity = new Vector2(horizontal * speed * Time.deltaTime, rigidbody2D.velocity.y);
        }
        //根据移动方向改变人物方向
        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce * Time.deltaTime);
        }
    }
}