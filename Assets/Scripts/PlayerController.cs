using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection;


    public float speed = 5f;
    public float jumpForce = 5f;
    public float gravity = 9.81f;

    private bool isJumping = false;
    public float jumpTime = 0.5f;
    private float jumpTimer = 0f;

    public float ExtraSpeed = 0 , ExtraJumpForce = 0;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0f,0f );
        moveDirection = transform.TransformDirection(moveDirection);
        if (ExtraSpeed > 0)
        {
            moveDirection *= (speed +ExtraSpeed);
        }
        else
        {
            moveDirection *= speed ;
        }

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                jumpTimer = 0f;
            }
        }

        if (isJumping)
        {
            jumpTimer += Time.deltaTime;

            if (jumpTimer < jumpTime)
            {
                moveDirection.y = jumpForce;
            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            moveDirection.y -= gravity;
        }
        controller.Move(moveDirection * Time.deltaTime);
        
    }
}

