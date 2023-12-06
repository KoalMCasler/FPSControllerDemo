using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FPS : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CharacterController playerController;
    [SerializeField]
    private Camera fpsCamera;
    [Header("Movement Settings")]
    private Vector3 walkInput;
    private Vector3 moveValue;
    [SerializeField]
    private float speedMultiplier;
    [SerializeField]
    private float sprintMultiplier;
    [SerializeField]
    private int jumpForce;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private bool isSprinting;
    private Vector3 speedVector;
    [Header("Look Settings")]
    [SerializeField]
    private float lookSensitivity;
    [SerializeField]
    private float upDownLimit;
    private float verticalRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerController = GetComponent<CharacterController>();
        player = gameObject;
        fpsCamera = Camera.main;
        if(speedMultiplier < 2 || speedMultiplier > 8)
        {
            speedMultiplier = 2;
        }
        if(jumpForce < 1)
        {
            jumpForce = 1;
        }
        if(gravity >= 0 || gravity < -10)
        {
            gravity = -1;
        }
        if(upDownLimit < 60 || upDownLimit > 80)
        {
            upDownLimit = 70;
        }
        if(sprintMultiplier <= 0)
        {
            sprintMultiplier = 1.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ManageLook();
        ManageInput();
        SpeedTest();
    }
    void ManageInput()
    {
        walkInput = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical")).normalized;
        walkInput = transform.rotation * walkInput;
        // Gets Jump input
        if(Input.GetButtonDown("Jump") /*&& isGrounded == true*/)
        {
            moveValue.y = jumpForce;
        }
        else
        {
            moveValue.y = gravity;
        }
        if(Input.GetButton("Sprint"))
        {
            isSprinting = true;
        }
        if(Input.GetButtonUp("Sprint"))
        {
           isSprinting = false;
        }
        if(isSprinting == true)
        {
            moveValue.x = walkInput.x * (speedMultiplier*sprintMultiplier);
            moveValue.z = walkInput.z * (speedMultiplier*sprintMultiplier);
        }
        if(isSprinting == false)
        {
            moveValue.x = walkInput.x * speedMultiplier;
            moveValue.z = walkInput.z * speedMultiplier;
        }
        // moves player
        playerController.Move(moveValue * Time.deltaTime);
    }
    void ManageLook()
    {
        float mouseXRotation = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseYRotation = Input.GetAxis("Mouse Y");
        player.transform.Rotate(0f,mouseXRotation,0f);
        verticalRotation -= mouseYRotation * lookSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation,-upDownLimit,upDownLimit);
        fpsCamera.transform.localRotation = Quaternion.Euler(verticalRotation,0,0);
    }
    void SpeedTest()
    {
        // Tests player speed
        speedVector = playerController.velocity;
        currentSpeed = speedVector.magnitude;
    }
}
