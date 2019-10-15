using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//usage: put this on an item that can be picked up and stacked on top of another
//intent: player can stack items on top of one another

public class StackItems : MonoBehaviour
{
    public TextMeshProUGUI stackText;
    bool isNear;
    public GameObject player; 

    void Start() {
        isNear = false;
    }
    // Update is called once per frame
    void Update()
    {
        Ray myRay = new Ray(transform.position, player.transform.forward); 
        float rayDistance = 1.5f;

        Debug.DrawRay(myRay.origin, myRay.direction * rayDistance, Color.green);
        RaycastHit rayHit = new RaycastHit();
        GameObject item = null;

        if (Physics.Raycast(myRay, out rayHit, rayDistance)) {
            //if it hits then you can pick up the item
            if (rayHit.collider.gameObject.tag == "Pickupable Item") {
                item = rayHit.collider.gameObject;
                isNear = true;
            }
        }
        else {
            isNear = false;
        }

        if (isNear && player.GetComponent<PlayerMovement>().holdingObject != null) {
            Debug.Log("changing stack text");
            stackText.text = "press 'X' to stack items"; 
        }
        else {
            stackText.text = "";
        }

        if (isNear && Input.GetKeyDown(KeyCode.X) && player.GetComponent<PlayerMovement>().holdingObject != null && item != null) {
            transform.position = item.transform.position + new Vector3(0f, 3f, 0f);
            player.GetComponent<PlayerMovement>().drop(gameObject);
            isNear = false;
        }    
    }
}
