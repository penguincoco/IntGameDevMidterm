using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on a cube with a Rigidbody
//intent: let player use WASD/arrows to move cube around

public class PlayerMovement : MonoBehaviour
{
    Rigidbody myRigidBody;
    Vector3 myInput; 

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //move with A/D or left/right
        float vertical = Input.GetAxis("Vertical"); //move with W/S or up/down

        //Translate does not account for physics or collision
        //transform.Translate() <-- no >:(

        myInput = horizontal * transform.right;
        myInput += vertical * transform.forward; 
    }

    //FixedUpdate is like Update(), but for physics! Runs at a different framerate than regular update
    void FixedUpdate() {
        //AddForce adds thrust 
        //myRigidBody.AddForce(myInput * 100f);
        myRigidBody.velocity = myInput * 10f;
    }
}
