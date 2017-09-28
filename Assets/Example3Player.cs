using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example3Player : MonoBehaviour {

    CharacterController pawn;
    public float baseSpeed = 8;
    public float dashSpeed = 36;
    private float moveSpeed;
    public float turnSpeed;
    public float impulseJump;
    public float baseGravityMulti;
    public float jumpGravityMulti;
    bool isJumping = false;
    bool isDashing = false;
    bool isCrouching = false;
    private float dashCD = 0;
    private float dashLength = .18f;

    Vector3 velocity = Vector3.zero;

    void Start () {
        pawn = GetComponent<CharacterController>();
        moveSpeed = baseSpeed;
	}
	
	void Update () {

        dashCD -= Time.deltaTime;
        if (dashCD <= 0) dashCD = 0;
        dashLength -= Time.deltaTime;
        if (dashLength <= 0) dashLength = 0;

        float axisV = Input.GetAxis("Vertical");
        float axisH = Input.GetAxis("Horizontal");

        transform.Rotate(0, axisH * turnSpeed * Time.deltaTime, 0);

        Vector3 move = transform.forward * axisV * moveSpeed;

        velocity.x = move.x;
        velocity.z = move.z;

        float gravityScale = baseGravityMulti;
        if (pawn.isGrounded)
        {
            velocity.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                velocity.y = impulseJump;
                gravityScale = jumpGravityMulti;
            }
            if (Input.GetButton("Crouch"))
            {
                isCrouching = true;
                transform.localScale = new Vector3(1, .5f, 1);
                if (isDashing)
                {
                    moveSpeed = dashSpeed;
                }
                else
                {
                    moveSpeed = baseSpeed / 2;
                }
                velocity.x = velocity.x * .99f;
                velocity.z = velocity.z * .99f;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                isCrouching = false;
                moveSpeed = baseSpeed;
                transform.localScale = new Vector3(1, 1, 1);
                transform.position = new Vector3(transform.position.x, transform.position.y + .45f, transform.position.z);
            }
        } else
        {
            if (Input.GetButton("Jump"))
            {
                if (isJumping && velocity.y > 0) gravityScale = jumpGravityMulti;                
            } else
            {
                isJumping = false;
            }
        }
        if (isDashing)
        {
            if (dashLength >= .15f)
            {
                moveSpeed = dashSpeed;
                dashCD = 1.5f;
            }
            else if (dashLength <= .15f && dashLength > 0)
            {
                moveSpeed -= Time.deltaTime * 70;
                if (moveSpeed <= baseSpeed) moveSpeed = baseSpeed;
            }
            else
            {
                moveSpeed = baseSpeed;
                isDashing = false;
            }
        }
        else if (!isDashing && Input.GetButtonDown("Dash") && dashCD == 0)
        {
            isDashing = true;
            dashLength = .18f;
        }
        velocity += Physics.gravity * Time.deltaTime * gravityScale;
        pawn.Move(velocity * Time.deltaTime);
	}
}
