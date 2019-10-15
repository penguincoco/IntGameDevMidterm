using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on a button
//intent: turn off Main Menu Canvas to activate How To Play Canvas and vice Versa

public class loadMenus : MonoBehaviour
{
    public Canvas mainMenu;
    public Canvas howTo; 

    public void loadHowTo() {
        mainMenu.gameObject.SetActive(false);
        howTo.gameObject.SetActive(true);
    }

    public void loadMainMenu() {
        mainMenu.gameObject.SetActive(true);
        howTo.gameObject.SetActive(false);
    }
}
