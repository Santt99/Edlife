using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMenu : MonoBehaviour {
    public KeyCode showingMenu, keyToPause;
    public Canvas menuItems, menuCreation, pauseMenu;
	// Use this for initialization
	void Start () {
        menuItems = menuItems.GetComponent<Canvas>();
        menuCreation = menuCreation.GetComponent<Canvas>();
        pauseMenu = pauseMenu.GetComponent<Canvas>();
        menuCreation.enabled = false;
        pauseMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(showingMenu))
        {
            menuItems.enabled = menuCreation.enabled;
            menuCreation.enabled = !menuCreation.enabled;
            
        }
        if (Input.GetKeyDown(keyToPause))
        {
            pauseMenu.enabled = !pauseMenu.enabled;
            if (pauseMenu.enabled)
            {
                Time.timeScale = 0;
                menuItems.enabled = false;
                menuCreation.enabled = false;
            }
            else
            {
                Time.timeScale = 1;
                menuItems.enabled = true;
            }
        }
        
	}
}
