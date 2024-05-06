using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonPress : MonoBehaviour
{
    // Start is called before the first frame update
    private InputAction paspaudimas;

    [SerializeField]
    private InputActionAsset asset;
    void Start()
    {
        InputActionMap map = asset.FindActionMap("XRI LeftHand Interaction", false);
        if (map == null)
            Debug.Log("Nerado map");

        paspaudimas = map.FindAction("Record");
        if (paspaudimas == null)
            Debug.Log("Nera veiksmo");
    }

    // Update is called once per frame
    void Update()
    {
        if (paspaudimas.triggered)
        {
            Debug.Log("Paspausta");
        }
    }
}