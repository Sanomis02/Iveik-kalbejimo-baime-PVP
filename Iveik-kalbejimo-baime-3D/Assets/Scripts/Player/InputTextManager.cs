using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputTextManager : MonoBehaviour
{

    public InputField input;
    
    private static bool isWriting = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            input.gameObject.SetActive(true);
            input.Select(); // Focus on the input field
            input.ActivateInputField(); 
            isWriting = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            input.gameObject.SetActive(false);
            isWriting = false;
        }

    }

    public bool getWrite()
    {
        return isWriting;
    }
}
