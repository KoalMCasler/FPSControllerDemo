using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FPS : MonoBehaviour
{
    private CharacterController player;
    public GameObject fpsCamera;
    private Vector3 walkInput;
    private Vector3 moveValue;
    public int speedMultiplier;
    public int jumpForce;
    private bool isGrounded;
    public float gravity;
    public float currentSpeed;
    private Vector3 speedVector;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
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
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
        SpeedTest();
    }
    void ManageInput()
    {
        // Gets Jump input
        if(Input.GetButtonDown("Jump"))
        {
            moveValue.y = jumpForce;
        }
        else
        {
            moveValue.y = gravity;
        }
        // Gets walk input
        walkInput = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical")).normalized;
        moveValue.x = walkInput.x * speedMultiplier;
        moveValue.z = walkInput.z * speedMultiplier;
        // moves player
        player.Move(moveValue * Time.deltaTime);
    }
    void SpeedTest()
    {
        // Tests player speed
        speedVector = player.velocity;
        currentSpeed = speedVector.magnitude;
    }
}
