using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on a cube with a Rigidbody
//intent: let player use WASD/arrows to move cube around

public class PlayerMovement : MonoBehaviour
{
    Rigidbody myRigidBody;
    Vector3 myInput; 
    bool canPickup;
    public GameObject objectDestination;
    GameObject[] pickupableItems;
    public Transform destination;
    float range;
    public GameObject toPickup;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        range = 1.5f;
        canPickup = true;
        pickupableItems = GameObject.FindGameObjectsWithTag("Pickupable Item");
        Debug.Log(pickupableItems.Length);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //move with A/D or left/right
        float vertical = Input.GetAxis("Vertical"); //move with W/S or up/down

        myInput = horizontal * transform.right;
        myInput += vertical * transform.forward; 

        //don't let the player pick up an item if they're already holding one 
        if (objectDestination.transform.childCount != 0) {
            Debug.Log("The player is currently holding something");
            canPickup = false; 
        }
        else {
            canPickup = true;
        }


        if (canPickup && Input.GetKey(KeyCode.E) && (destination.transform.position - toPickup.transform.position).sqrMagnitude < range * range) {
            pickup(toPickup);
        }
        else {
            drop(toPickup);
        }
    }

    void FixedUpdate() {
        myRigidBody.velocity = myInput * 10f;
    }

    void pickup(GameObject item) {
        Debug.Log("item being picked up");
        GetComponent<Rigidbody>().useGravity = false;
        item.transform.position = destination.position;
        item.transform.parent = GameObject.Find("Destination").transform;
        canPickup = false;
    }

    void drop(GameObject item) {
        canPickup = true;
        //Debug.Log("Dropping item");
        GetComponent<Rigidbody>().useGravity = true;
        item.transform.parent = null;
        canPickup = true;
    }
}
