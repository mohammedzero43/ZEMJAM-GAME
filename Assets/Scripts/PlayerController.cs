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

    public bool isMoving, isJumping, isRunning;

    private float X, Y;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        cc.enabled = true;

        //this part is the locking of cursor and to make it visible or no
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    private void Update()
    {
        #region Camera Limitation Calculator
        //Camera limitation variables
        const float MIN_Y = -60.0f;
        const float MAX_Y = 70.0f;

        X += Input.GetAxis("Mouse X") * (Sensitivity * Time.deltaTime);
        Y -= Input.GetAxis("Mouse Y") * (Sensitivity * Time.deltaTime);

        if (Y < MIN_Y)
            Y = MIN_Y;
        else if (Y > MAX_Y)
            Y = MAX_Y;
        #endregion
        transform.localRotation = Quaternion.Euler(Y, X, 0.0f);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        moveDirection *= movementSpeed;

        if (cc.isGrounded)
        {
            isJumping = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);
    }
}

