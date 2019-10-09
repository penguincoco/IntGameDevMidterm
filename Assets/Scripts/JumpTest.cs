using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{
    //variables for handling the jump
    public float jumpForce = 5f;
    Rigidbody myRigidBody;

    //ground check variables
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        isGrounded = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            myRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        Ray myRay = new Ray(transform.position, Vector3.down);

        float myRayDistance = 1.1f;

        Debug.DrawRay(myRay.origin, myRay.direction * myRayDistance, Color.blue);
        
        if (Physics.Raycast(myRay, myRayDistance)) {
            Debug.Log("Raycast returns true!"); 
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }
    }
}
