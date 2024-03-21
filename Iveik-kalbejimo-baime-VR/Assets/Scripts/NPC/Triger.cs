using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Triger : MonoBehaviour
{
    // titrai
    [SerializeField] private Canvas titrai;
    [SerializeField] private TMP_Text pranesimas;
    [SerializeField] private float startLaikasPranessimo;

    private bool rodo = false;
    private float LaikasPranessimo; 

        void Start()
    {
        LaikasPranessimo = startLaikasPranessimo; // Initialize LaikasPranessimo with starting value
    }
  void Update()
    {
        if (rodo)
        {
            // Update the timer while the text is being displayed
            LaikasPranessimo -= Time.deltaTime;

            // If the timer runs out, hide the text
            if (LaikasPranessimo <= 0)
            {
                pranesimas.gameObject.SetActive(false);
                rodo = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Player")) {
        Debug.Log("Enter");
        titrai.gameObject.SetActive(true);
        LaikasPranessimo = startLaikasPranessimo;
         

      if (!rodo)
            {
              pranesimas.gameObject.SetActive(true);
              rodo = true;
            }

      }
    }
 
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT");
        titrai.gameObject.SetActive(false);
    }
 

}