using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on a cube with a Rigidbody
//intent: let player use WASD/arrows to move cube around

public class PlayerMovement : MonoBehaviour
{
    //variables to handle player movement
    Rigidbody myRigidBody;
    Vector3 myInput; 
    bool isGrounded; 
    //Vector3 jump;
    public float jumpForce; 

    //variables to handle player picking up an object
    bool canPickup;
    bool isHolding;
    private GameObject holdingObject;
    public Transform destination;
    float speed;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        canPickup = false;
        jumpForce = 5f;
        isGrounded = true;
        speed = 5f;
    }

    void Update()
    {
        //handle basic player movement with WASD/arrow keys 
        float horizontal = Input.GetAxis("Horizontal"); //move with A/D or left/right
        float vertical = Input.GetAxis("Vertical"); //move with W/S or up/down
        float jump = Input.GetAxis("Jump");

        myInput = horizontal * transform.right;
        myInput += vertical * transform.forward; 

        //handle player jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            
            myRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        Ray jumpRay = new Ray(transform.position, Vector3.down);

        float jumpRayDist = 1.1f;

        Debug.DrawRay(jumpRay.origin, jumpRay.direction * jumpRayDist, Color.blue);
        
        if (Physics.Raycast(jumpRay, jumpRayDist)) {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }

        //handle player picking up and dropping objects
        Ray myRay = new Ray(transform.position, transform.forward); 
        float rayDistance = 2f;

        Debug.DrawRay(myRay.origin, myRay.direction * rayDistance, Color.blue);

        RaycastHit rayHit = new RaycastHit();
        GameObject item = null;

        if (Physics.SphereCast(myRay, 0.5f, out rayHit, rayDistance)) {
            //if it hits then you can pick up the item
            if (rayHit.collider.gameObject.tag == "Pickupable Item") {
                item = rayHit.collider.gameObject;
                canPickup = true;
            }
        }
        else {
            canPickup = false;
        }

        if (canPickup && holdingObject == null && Input.GetKeyDown(KeyCode.E)) {
            pickup(item);
        }
        else if (holdingObject != null && Input.GetKeyDown(KeyCode.E)) {
            drop(holdingObject);
        }

        //cooldown after dropping an iteam that another one can't be picked up and
        //prevent spamming the stack button so the boxes don't keep flying up before hitting
        //the ground again
    }

    void FixedUpdate() {
        // myRigidBody.AddForce( myInput * 100f );
        //myRigidBody.velocity = myInput * 10f;
        myRigidBody.velocity = new Vector3(myInput.x * speed, myRigidBody.velocity.y, myInput.z * speed);
    }

    void pickup(GameObject item) {
        holdingObject = item;
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.position = destination.position;
        item.transform.parent = GameObject.Find("Destination").transform;
    }

    public void drop(GameObject item) {
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        holdingObject = null;
    }
 }