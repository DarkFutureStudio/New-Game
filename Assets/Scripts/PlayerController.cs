﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //*************************************/  Components on the player gameobject
    private CapsuleCollider2D cc;
    private Rigidbody2D rb;
    //*************************************/

    //*************************************/ variables for moving player
    [SerializeField] private float speed;
    private float moveInput;
    //************************************/

    //************************************/  variables for jump settings
    [SerializeField] private float checkRadius;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool isGrounded;
    private float jumpTimeCounter;
    private bool isJumping;
    /**************************************/

    //************************************/ variables for shapeshifting
    public Sprite bird;
    private SpriteRenderer sp;
    private BirdController bc;
    public Vector3 birdScale;
    /*************************************/

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        bc = GetComponent<BirdController>();
        cc = GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius,whatIsGround);

        if(isGrounded == true && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }

        if(Input.GetButton("Jump") && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false;
        }

        if(Input.GetButtonUp("Jump"))
            isJumping = false;

        if(Input.GetButtonDown("ShapeShift"))
            shapeShift();
    }
    void FixedUpdate() => move();

    private void move()
    {
        rb.velocity = new Vector2(moveInput * speed,rb.velocity.y);

        if(moveInput > 0)
            transform.eulerAngles = new Vector3(0,0,0);
        else if(moveInput < 0)
            transform.eulerAngles = new Vector3(0,180,0);
    }

    private void shapeShift()
    {
        this.enabled = false;
        bc.enabled = true;
    }

    // private void OnDisable()
    // {
    //     cc.size = new Vector2(4.8f,7.4f);
    //     rb.gravityScale = 0;
    //     transform.localScale = birdScale;
    //     sp.sprite = bird;
    // }
}
