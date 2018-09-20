using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperPlayerMovement : MonoBehaviour
{

    CharacterController cc;
    Transform tf;
    Animator pa;

    Vector3 movement = Vector3.zero;

    float moveSpeed = 1f;
    float rotateSpeed = 100f;

    float sprintMultiplier = 1.75f;
    float crouchSpeedMultiplier = .5f;
    float proneSpeedMultiplier = .1f;

    bool isCrouching = false;
    bool isProne = false;
    bool isClimbing = false;

    GameObject Climbable;
    GameObject Clamberable;

    public GameObject ClamberDestination;

    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
        tf = GetComponent<Transform>();
        pa = GetComponent<Animator>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Walking
        Walking();

        //Sprinting
        Sprinting();

        //Crouching and proning
        CrouchAndProne();

        //Climbing and falling
        ClimbingAndGravity();

        //Shooting
        if (Input.GetMouseButton(0))
        {
            Shooting();
        }
        //Clambering
        //Clambering();

        //Movement
        movement = transform.TransformDirection(movement);
        movement *= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //Movement Input
        cc.Move(movement);

        //Camera Rotation
        this.gameObject.transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0);
        Camera.main.transform.Rotate(Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime * -1, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Climbable")
            Climbable = other.gameObject;

        //if (other.gameObject.tag == "Clamberable")
        //    Clamberable = other.gameObject;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Climbable")
            Climbable = null;

        //if (other.gameObject.tag == "Clamberable")
        //    Clamberable = null;
    }

    private void Walking()
    {
        if (!isClimbing)
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movement *= moveSpeed;
        }
    }

    private void Sprinting()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0 && !isCrouching && !isProne && !isClimbing)
        {
            movement.z *= sprintMultiplier;
        }
    }

    private void CrouchAndProne()
    {
        if (Input.GetKey(KeyCode.C) && !isClimbing) //Crouch Input
        {
            isCrouching = true;
            tf.localScale = Vector3.Lerp(tf.localScale, new Vector3(tf.localScale.x, .6f, tf.localScale.z), .3f);
            movement *= crouchSpeedMultiplier;
        }
        else
        {
            isCrouching = false;
        }

        if (Input.GetKey(KeyCode.X) && !isClimbing) //Prone Input
        {
            isProne = true;
            tf.localScale = Vector3.Lerp(tf.localScale, new Vector3(tf.localScale.x, .2f, tf.localScale.z), .1f);
            movement *= proneSpeedMultiplier;
        }
        else
        {
            isProne = false;
        }

        if (!isProne && !isCrouching)
        {
            isProne = false;
            isCrouching = false;
            tf.localScale = Vector3.Lerp(tf.localScale, new Vector3(1, 1, 1), .5f);
        }
    }

    private void ClimbingAndGravity()
    {
        if (Climbable != null && Input.GetKeyDown(KeyCode.E))
        {
            isClimbing = !isClimbing;
        }

        if (Climbable != null && isClimbing == true)
        {
            if (Input.GetKey(KeyCode.W) && !isProne && !isCrouching)
            {
                movement += -Physics.gravity * .5f;

            }
            else if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && !isProne && !isCrouching))
            {
                movement += Physics.gravity * .5f;
            }
            else
            {
                movement += Physics.gravity * 0f;
            }
        }
        else
        {
            movement += Physics.gravity * 1f;
            isClimbing = false;
        }
    }

    private void Clambering()
    {
        if (Clamberable != null && Input.GetKeyDown(KeyCode.Space))
        {
            tf.position = Vector3.Lerp(tf.position, ClamberDestination.transform.position, 1f);
        }
    }

    private void Shooting()
    {
        Ray rayOrigin = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        //RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, Mathf.Infinity))
        {
            Debug.Log("Hit: Anything");
        }
    }
}