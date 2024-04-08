using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject hubMenu, nustatymuMenu;
    bool isOn = false;
    public void OnToggleNustatymaiClick()
    {
        isOn = !isOn;
        if (isOn)
        {
            nustatymuMenu.SetActive(true);
        }
        else
            nustatymuMenu.SetActive(false);
    }
}
