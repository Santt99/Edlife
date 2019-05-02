using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingMenu : MonoBehaviour {
    public GameObject otherMenu;

    public void ChangingThroughMenus()
    {
        this.gameObject.SetActive(false);
        otherMenu.SetActive(true);
    }
}
