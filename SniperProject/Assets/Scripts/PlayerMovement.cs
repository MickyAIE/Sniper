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

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
        tf = GetComponent<Transform>();

        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement *= moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0)
        {
            movement.z *= sprintMultiplier;
        }
        

        movement = transform.TransformDirection(movement);

        movement += Physics.gravity;
    }

    private void FixedUpdate()
    {
        cc.Move(movement);

        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0);
        Camera.main.transform.Rotate(Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime * -1, 0, 0);
    }
}
