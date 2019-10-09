using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//usage: put this on an item that can be stacked on top of another
//intent: player can stack items on top of one another

public class StackItems : MonoBehaviour
{
    GameObject[] stackableItems; 
    public TextMeshProUGUI stackText;
    bool isNear;
    public GameObject player; 

    void Start() {
        stackableItems = GameObject.FindGameObjectsWithTag("Pickupable Item");
        isNear = false;
    }
    // Update is called once per frame
    void Update()
    {
        //each box will do a raycast as well to chek if they are near another object that
        //they can be stacked on top of

        Ray myRay = new Ray(transform.position, transform.forward); 
        float rayDistance = 3f;

        Debug.DrawRay(myRay.origin, myRay.direction * rayDistance, Color.green);
        RaycastHit rayHit = new RaycastHit();
        GameObject item = null;

        if (Physics.Raycast(myRay, out rayHit, rayDistance)) {
            //if it hits then you can pick up the item
            if (rayHit.collider.gameObject.tag == "Pickupable Item") {
                Debug.Log("Near another object");
                item = rayHit.collider.gameObject;
                isNear = true;
            }
        }

        if (isNear) {
            stackText.text = "press x to stack items"; 
        }

        if (isNear && Input.GetKeyDown(KeyCode.X)) {
            Debug.Log("Stacking items");
            transform.position = item.transform.position + new Vector3(0f, 3f, 0f);
            //access the playermovement script and call Drop()
            player.GetComponent<PlayerMovement>().drop(gameObject);
        }
    }
}
