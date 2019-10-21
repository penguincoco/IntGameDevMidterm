using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//usage: put this on an object that will never be destroyed
//intent: keep track of the number of items in a scene that a player has picked up

public class PickUpManager : MonoBehaviour
{
    List<GameObject> winItems;
    public GameObject player;
    public AudioSource pickupPlayer;

    public Canvas winCanvas;

    void Start() {
        winItems = new List<GameObject>();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Win Item")) {
            winItems.Add(item);
        }
    }

    void Update()
    {
        if (winItems.Count == 0) {
            this.enabled = false;
            winCanvas.gameObject.SetActive(true);
            Camera.main.GetComponent<MouseLook>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
        }
    }

    void OnTriggerEnter(Collider otherObj) {
        if (otherObj.tag == "Win Item") {
            pickupPlayer.Play();
            winItems.Remove(otherObj.gameObject);
            Destroy(otherObj.gameObject);
        }
    }
}
