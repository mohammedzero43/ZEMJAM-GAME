using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private float Sensitivity;
    [SerializeField] Vector3 moveDirection;

    public float movementSpeed = 5f;
    public float jumpForce = 5f;


    private float X, Y;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        cc.enabled = true;

    }
    private void FixedUpdate()
    {
 

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        moveDirection *= movementSpeed;

        if (cc.isGrounded)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        cc.Move(moveDirection * Time.fixedDeltaTime);
    }
}

