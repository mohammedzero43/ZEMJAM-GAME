using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    public float jumpForce = 5f;
    public bool isJumping = false;

    public float ExtraSpeed = 0, ExtraJumpForce = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        // Horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");

            Vector3 movement = new Vector3(horizontalInput * (speed+ ExtraSpeed), rb.velocity.y, rb.velocity.z);
            rb.velocity = movement ;

        // Jump
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector3(0f, (jumpForce+ ExtraJumpForce), 0f), ForceMode.Impulse);
            rb.AddForce(new Vector3(0f, -jumpForce/2, 0f), ForceMode.Impulse);
            isJumping = true;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer==6)
        {
            isJumping = false;
        }
    }

}
