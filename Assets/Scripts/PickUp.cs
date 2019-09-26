using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage:
//intent: 
public class PickUp : MonoBehaviour
{
    public Transform destination; 
    bool isPressed;
    float range;

    void Start() {
        range = 1.5f;
    }

    void Update() {
        if (Input.GetKey(KeyCode.E) && (destination.transform.position - transform.position).sqrMagnitude < range * range) {
            pickup();
        }
        // if (isPressed) {
        //     GetComponent<Rigidbody>().useGravity = false;
        //     this.transform.position = destination.position;
        //     this.transform.parent = GameObject.Find("Destination").transform;
        // }
        else {
            drop();
        }
    }

    void pickup() {
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = destination.position;
        this.transform.parent = GameObject.Find("Destination").transform;
    }

    void drop() {
        GetComponent<Rigidbody>().useGravity = true;
    }

    // void OnMouseDown() {
    //     isPressed = true;
    // }

    // void OnMouseUp() {
    //     isPressed = false;
    // }
}
