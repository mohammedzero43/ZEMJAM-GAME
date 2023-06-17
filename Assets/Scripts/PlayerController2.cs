using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    public float jumpForce = 5f;
    public bool isJumping = false;

    public static float ExtraSpeed = 0, ExtraJumpForce = 0;
    public static bool isRampage;
    public bool isdead = false;
    public Vector3 originalPos;
    void Start()
    {
        originalPos = this.transform.position;
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        if (!isdead)
        {
            // Horizontal movement
            float horizontalInput = -Input.GetAxis("Horizontal");

            Vector3 movement = new Vector3(horizontalInput * (speed + ExtraSpeed), rb.velocity.y, rb.velocity.z);
            rb.velocity = movement;

            // Jump
            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                rb.AddForce(new Vector3(0f, (jumpForce + ExtraJumpForce), 0f), ForceMode.Impulse);
                rb.AddForce(new Vector3(0f, -jumpForce / 2, 0f), ForceMode.Impulse);
                isJumping = true;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer==6)
        {
            isJumping = false;
        }
        if (collision.gameObject.layer == 8)
        {
            if (isRampage) 
            {
                collision.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
                isdead = true;
                StartCoroutine(waitor());
        }
      /*  if (other.gameObject.layer == 7)
        {
            isdead = true;
            StartCoroutine(waitor());
        }*/
    }
    IEnumerator waitor() 
    {
        yield return new WaitForSeconds(0.5f);
        this.transform.position = originalPos;
        isdead = false;

    }


}
