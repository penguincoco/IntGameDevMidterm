using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//usage: put this on an item that can be stacked on top of another
//intent: player can stack items on top of one another

public class StackItems : MonoBehaviour
{
    public GameObject otherBox; 
    GameObject[] stackableItems; 
    public TextMeshProUGUI stackText; 
    float range;

    void Start() {
        stackableItems = GameObject.FindGameObjectsWithTag("Pickupable Item");
        range = 1.5f;
    }
    // Update is called once per frame
    void Update()
    {
        if ((otherBox.transform.position - transform.position).sqrMagnitude < range * range) {
            Debug.Log("two boxes are close together");
            stackText.text = "press x to stack items"; 
            if (Input.GetKey(KeyCode.X)) {
                Debug.Log("stacking one box on top of another");
                transform.position = otherBox.transform.position + new Vector3(0f, 1f, 0f);
            }
        }
        else {
            stackText.text = "";
        }
        //if two stackable items are near each other, display text saying 
        //press 'r' to stack items
        // foreach (GameObject item in stackableItems) {
        //     if ((item.transform.position - transform.position).sqrMagnitude < range * range) {
        //         Debug.Log("player is within range to stack an item");
        //         stackText.text = "press x to stack items"; 
                
        //         if (Input.GetKey(KeyCode.X)) {
        //             stackItem();
        //         }
        //         //stackItem();
        //     }
        // }
    }

    void stackItem() {

    }
}
