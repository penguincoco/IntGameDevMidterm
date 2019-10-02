using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    float verticalAngle = 0f;

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
        
        //returns 0 if we aren't moving the mouse
        float mouseX = Input.GetAxis("Mouse X"); //horizontal mouse velocity
        float mouseY = Input.GetAxis("Mouse Y"); //vertical mouse velocity 

        transform.parent.Rotate(0f, mouseX * 10f, 0f);

        verticalAngle -= mouseY * 10f; 
        verticalAngle = Mathf.Clamp(verticalAngle, -80f, 80f);

        // X = pitch, Y = yaw, Z = Roll 
        transform.localEulerAngles = new Vector3(verticalAngle, transform.localEulerAngles.y, 0f);
    }
}

