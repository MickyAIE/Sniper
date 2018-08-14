using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    
    CharacterController cc;
    Transform tf;

    Vector3 movement = Vector3.zero;
    Vector3 rotation = Vector3.zero;

    float moveSpeed = .1f;
    float rotateSpeed = 100f;

    float sprintMultiplier = 1.75f;
    float crouchSpeedMultiplier = .5f;
    float proneSpeedMultiplier = .1f;

    bool isCrouching = false;
    bool isProne = false;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
        tf = GetComponent<Transform>();

        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        //Walking Input
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement *= moveSpeed;

        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0 && !isCrouching && !isProne)
        {
            movement.z *= sprintMultiplier;
        }
        


        if (Input.GetKey(KeyCode.C) && !isProne) //Crouch Input
        {
            isCrouching = true;
            tf.localScale = new Vector3(tf.localScale.x, .4f, tf.localScale.z);
            movement *= crouchSpeedMultiplier;
        }
        if (Input.GetKey(KeyCode.X)) //Prone Input
        {
            isProne = true;
            tf.localScale = new Vector3(tf.localScale.x, .2f, tf.localScale.z);
            movement *= proneSpeedMultiplier;
        }
        else
        {
            isProne = false;
            isCrouching = false;
            tf.localScale = new Vector3(1f, 1f, 1f);
        }

        //Movement
        movement = transform.TransformDirection(movement);
        movement += Physics.gravity;
        
    }

    private void FixedUpdate()
    {
        //Movement Input
        cc.Move(movement);

        //Camera Rotation
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0);
        Camera.main.transform.Rotate(Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime * -1, 0, 0);
    }
}