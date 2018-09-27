using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    CharacterController cc;
    Transform tf;

    [SerializeField]
    private GameObject enemyGeneral;

    AudioSource _audio;

    

    CharHealth _charH;

    Vector3 movement = Vector3.zero;
    Vector3 rotation = Vector3.zero;

    float moveSpeed = .50f;
    float rotateSpeed = 100f;

    float sprintMultiplier = 4.50f;
    float crouchSpeedMultiplier = .5f;
    float proneSpeedMultiplier = .1f;
    Bot _enemyHealth;
    bool isCrouching = false;
    bool isProne = false;

    [SerializeField] GameObject Climbable;
    
	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
        tf = GetComponent<Transform>();
        _charH = GetComponent<CharHealth>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        

        _audio = GetComponent<AudioSource>();
        _enemyHealth = GameObject.Find("General").GetComponent<Bot>();

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

        //Shooting
        if (Input.GetMouseButtonDown(0))
        {
            _audio.Play();
            Shooting();
            
        }


        if (Input.GetKey(KeyCode.C)) //Crouch Input
        {
            isCrouching = true;
            tf.localScale = Vector3.Lerp(tf.localScale, new Vector3(tf.localScale.x, .6f * 4.5f, tf.localScale.z), .3f);
            movement *= crouchSpeedMultiplier;
        }
        else
        {
            isCrouching = false;
        }

        if (Input.GetKey(KeyCode.X)) //Prone Input
        {
            isProne = true;
            tf.localScale = Vector3.Lerp(tf.localScale, new Vector3(tf.localScale.x, .2f * 4.5f, tf.localScale.z), .1f);
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
            tf.localScale = Vector3.Lerp(tf.localScale, new Vector3(4.5f, 4.5f, 4.5f), .5f);
        }


        if (Climbable != null && Input.GetKey(KeyCode.W) && !isProne && !isCrouching)
        {
            movement += -Physics.gravity * .01f;
        }
        else
        {
            movement += Physics.gravity * .03f;
        }

        //Movement
        movement = transform.TransformDirection(movement);
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
        {
            Climbable = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Climbable")
        Climbable = null;
    }

    float maxRange = 150;
    float damage = 100;
    private void Shooting()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, maxRange))
        {
            if (hitInfo.collider.gameObject.tag == "Enemy")
            {
                     
                    _enemyHealth.GetComponent<Bot>().health -= 10f;

                    Debug.Log("ENEMY HIT");
                
            }
        }
    }

}