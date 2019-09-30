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
        else {
            drop();
        }
    }

    void pickup() {
        Debug.Log("picking up item. Debug statement coming from pickup script");
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = destination.position;
        this.transform.parent = GameObject.Find("Destination").transform;
    }

    void drop() {
        //Debug.Log("Dropping item");
        GetComponent<Rigidbody>().useGravity = true;
        this.transform.parent = null;
    }
}
