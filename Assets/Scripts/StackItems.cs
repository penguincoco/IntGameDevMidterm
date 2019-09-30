using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
