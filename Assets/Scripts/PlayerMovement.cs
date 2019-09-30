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


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
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
        
        //current issue: this turns off the script in all the items that can
        //be picked up, which means this will turn off the script on the cube
        //that is being held which is handling the dropping
        // if (!canPickup) {
        //     Debug.Log("player is holding something and can't pick anything else up");
        //     foreach (GameObject item in pickupableItems) {
        //         item.GetComponent<PickUp>().enabled = false;
        //     }    
        // }    
    }

    void FixedUpdate() {
        myRigidBody.velocity = myInput * 10f;
    }
}
