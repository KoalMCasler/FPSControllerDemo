using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private CharacterController player;
    public float inputZ;
    public float inputX;
    private Vector3 moveInput;
    public int speedMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
    }
    void ManageInput()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"))* speedMultiplier * Time.deltaTime; 
        }
        else
        {
            moveInput = new Vector3(0f,0f, 0f);
        }
        player.Move(moveInput);
    }
}
