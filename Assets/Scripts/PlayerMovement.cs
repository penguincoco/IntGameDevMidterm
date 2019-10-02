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

    //variables to handle player picking up an object
    bool canPickup;
    bool isHolding;
    private GameObject holdingObject;

    List<GameObject> pickupableItems;
    public Transform destination;
    float range;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        range = 1.5f;
        canPickup = false;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //move with A/D or left/right
        float vertical = Input.GetAxis("Vertical"); //move with W/S or up/down

        myInput = horizontal * transform.right;
        myInput += vertical * transform.forward; 

        Ray myRay = new Ray(transform.position, transform.forward); 
        float rayDistance = 2f;

        Debug.DrawRay(myRay.origin, myRay.direction * rayDistance, Color.blue);

        RaycastHit rayHit = new RaycastHit();
        GameObject item = null;

        if (Physics.Raycast(myRay, out rayHit, rayDistance)) {
            //if it hits then you can pick up the item
            if (rayHit.collider.gameObject.tag == "Pickupable Item") {
                Debug.Log("Can pick up an item!");
                item = rayHit.collider.gameObject;
                canPickup = true;
            }
        }
        else {
            canPickup = false;
        }

        if (canPickup && Input.GetKey(KeyCode.E)) {
            pickup(item);
        }
        else if (holdingObject != null && Input.GetKeyUp(KeyCode.E)) {
            drop(holdingObject);
        }

        //cooldown after dropping an iteam that another one can't be picked up and
        //prevent spamming the stack button so the boxes don't keep flying up before hitting
        //the ground again
    }

    void FixedUpdate() {
        myRigidBody.velocity = myInput * 10f;
    }

    void pickup(GameObject item) {
        holdingObject = item;
        Debug.Log("item being picked up");
        item.GetComponent<Rigidbody>().useGravity = false;
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.transform.position = destination.position;
        item.transform.parent = GameObject.Find("Destination").transform;
    }

    void drop(GameObject item) {
        item.GetComponent<Rigidbody>().useGravity = true;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        holdingObject = null;
    }
}
