using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//usage: put this on a cube with a Rigidbody
//intent: control all aspects of player movement including: directional movement, jumping, picking up items 

public class PlayerMovement : MonoBehaviour
{
    //variables to handle player movement
    Rigidbody myRigidBody;
    Vector3 myInput; 
    bool isGrounded; 
    public float jumpForce; 

    //variables to handle player picking up an object
    bool canPickup;
    public GameObject holdingObject;
    public Transform destination;
    float speed;

    //variable for setting command text when a player is near an object that they can pick up
    public TextMeshProUGUI pickupText;

    //public AudioSource footstepPlayer;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        canPickup = false;
        jumpForce = 7.5f;
        isGrounded = true;
        speed = 5f;
    }

    void Update()
    {
        //handle basic player movement with WASD/arrow keys 
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical"); 

        myInput = horizontal * transform.right;
        myInput += vertical * transform.forward; 

        //handle player jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            myRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        Ray jumpRay = new Ray(transform.position, Vector3.down);

        float jumpRayDist = 1.3f;

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
            pickupText.text = "";
        }

        if (canPickup) {
            pickupText.text = "press 'E' to pick up item";
        }
        if (holdingObject) {
            pickupText.text = "";
        }

        if (canPickup && holdingObject == null && Input.GetKeyDown(KeyCode.E) && item != null) {
            pickup(item);
        }
        else if (holdingObject != null && Input.GetKeyDown(KeyCode.E)) {
            drop(holdingObject);
        }
    }

    void FixedUpdate() {
        myRigidBody.velocity = new Vector3(myInput.x * speed, myRigidBody.velocity.y, myInput.z * speed);
    }

    void pickup(GameObject item) {
        pickupText.text = "";
        holdingObject = item;
        Debug.Log(item.name);
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().isKinematic = true;

        GameObject destinationObj = GameObject.Find("Destination");
        
        item.GetComponent<BoxCollider>().enabled = false; 
        destinationObj.GetComponent<BoxCollider>().enabled = true;

        item.transform.position = destination.position;
        item.transform.parent = GameObject.Find("Destination").transform;

        item.GetComponent<StackItems>().enabled = true;
    }

    //made public so StackItems can call this function
    public void drop(GameObject item) {
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        holdingObject = null;

        GameObject destinationObj = GameObject.Find("Destination");
        
        item.GetComponent<BoxCollider>().enabled = true; 
        destinationObj.GetComponent<BoxCollider>().enabled = false;

        item.GetComponent<StackItems>().enabled = false;
    }
 }