using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{
    public GameObject menu;

    public void OpenMenu()
    {
        if(menu != null)
        {
            bool isActive = menu.activeSelf;
            menu.SetActive(!isActive);
        }
    }
}
