using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//---------------
//- This script moves the player
//- By: Peter Schreuder
//---------------

public class PlayerMovement : MonoBehaviour
{
    public bool isEnabled = true;

    public float moveSpeed = 0.4f, gravity = 20f;

    [SerializeField]
    private Vector3 axesScale = new Vector3(1, 1, 1);

    private Transform cameraTransform;

    public Rigidbody rigidbodyPlayer;

    private Vector3 moveDirection = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        //Find the current camera
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        rigidbodyPlayer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        //If the player can move on its own
        if (isEnabled)
            MoveDirection(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
        else// For the transition the players needs to keep moving
            MoveDirection(new Vector3(0f, 0f, 1f));


    }

    void MoveDirection(Vector3 _input)
    {
        moveDirection = _input;
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;


        rigidbodyPlayer.AddForce(Vector3.Scale(moveDirection, axesScale), ForceMode.Impulse);
    }

}
