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
    List<GameObject> pickupableItems;
    public Transform destination;
    float range;
    public GameObject toPickup;
    public GameObject secondItem;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        range = 1.5f;
        canPickup = true;
        isHolding = false;
        pickupableItems = new List<GameObject>();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Pickupable Item")) {
            pickupableItems.Add(item);
        }
        //pickupableItems = GameObject.FindGameObjectsWithTag("Pickupable Item");
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //move with A/D or left/right
        float vertical = Input.GetAxis("Vertical"); //move with W/S or up/down

        myInput = horizontal * transform.right;
        myInput += vertical * transform.forward; 

        foreach (GameObject item in pickupableItems) {
            //detect when an item is picked up
            if (Input.GetKey(KeyCode.E) && (destination.transform.position - item.transform.position).sqrMagnitude < range * range) {
                //PickUp.cs will execute the pickup 
                //remove and store that item that's being picked up from the list, turn off all of the
                //scripts for the other items to prevent being able to pick things up
                GameObject pickedUpItem = item;
                pickupableItems.Remove(item);
                foreach (GameObject otherItems in pickupableItems) {
                    otherItems.GetComponent<PickUp>().enabled = false;
                    Debug.Log("Accessing the scripts in other gameobjects");
                }
            }
            // else {
            //     drop(item);
            // }
        }


        // if (canPickup && Input.GetKey(KeyCode.E) && (destination.transform.position - toPickup.transform.position).sqrMagnitude < range * range) {
        //     pickup(toPickup);
        // }
        // else if (isHolding) {
        //     drop(toPickup);
        // }
    }

    void FixedUpdate() {
        myRigidBody.velocity = myInput * 10f;
    }

    void pickup(GameObject item) {
        Debug.Log("item being picked up");
        GetComponent<Rigidbody>().useGravity = false;
        item.transform.position = destination.position;
        item.transform.parent = GameObject.Find("Destination").transform;
    }

    void drop(GameObject item) {
        canPickup = true;
        //Debug.Log("Dropping item");
        GetComponent<Rigidbody>().useGravity = true;
        item.transform.parent = null;
    }
}
