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
    public TextMeshProUGUI winText;
    public AudioSource pickupPlayer;

    void Start() {
        winItems = new List<GameObject>();
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Win Item")) {
            winItems.Add(item);
        }
        Debug.Log(winItems.Count);
    }

    void Update()
    {
        Debug.Log(winItems.Count);
        if (winItems.Count == 0) {
            player.GetComponent<PlayerMovement>().enabled = false;
            winText.text = "Congrats! \nYou've won! \n Press '1' to restart";
            this.enabled = false;
        }
    }

    void OnTriggerEnter(Collider otherObj) {
        if (otherObj.tag == "Win Item") {
            pickupPlayer.Play();
            winItems.Remove(otherObj.gameObject);
            Debug.Log(winItems.Count);
            Destroy(otherObj.gameObject);
        }
    }
}
