using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuControllerTest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    public InputActionReference openMenuAction;
    public GameObject XRrigPos;
    const float menuDistance = 0.45f;
    public void Awake()
    {
        openMenuAction.action.Enable();
        openMenuAction.action.performed += ToggleMenu;
        InputSystem.onDeviceChange += OnDeviceChange;
    }



    private void OnDestroy()
    {
        openMenuAction.action.Disable();
        openMenuAction.action.performed -= ToggleMenu;
        InputSystem.onDeviceChange -= OnDeviceChange;

    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        menu.transform.position = XRrigPos.transform.position;
        menu.transform.Translate(0, menuDistance, menuDistance, Space.Self);

        menu.SetActive(true);
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Disconnected:
                openMenuAction.action.Disable();
                openMenuAction.action.performed -= ToggleMenu;
                break;
            case InputDeviceChange.Reconnected:
                openMenuAction.action.Enable();
                openMenuAction.action.performed += ToggleMenu;
                break;
        }
    }
}
