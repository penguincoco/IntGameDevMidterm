using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public Canvas gameMenu;
    public GameObject player; 

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            gameMenu.gameObject.SetActive(true);
            Camera.main.GetComponent<MouseLook>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
        }
    }

    public void loadMainMenu() {
        SceneManager.LoadScene("MenuScene");
    }

    public void closeMenu() {
        gameMenu.gameObject.SetActive(false);
        Camera.main.GetComponent<MouseLook>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        Cursor.visible = false;
    }

    public void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
