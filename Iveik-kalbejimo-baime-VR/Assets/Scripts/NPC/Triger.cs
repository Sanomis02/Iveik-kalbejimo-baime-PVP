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
    [SerializeField] private ChatGPT chatGPT;
   [SerializeField]private Animator animator;
    [SerializeField] private GameObject vaikas;

    //private Animator animator;
    private bool rodo = false;
    private float LaikasPranessimo; 
 
//private bool isAnimating = false;
        void Start()
    {
        animator = vaikas.GetComponent<Animator>();
      // bool isAnimating = false;
        LaikasPranessimo = startLaikasPranessimo; // Initialize LaikasPranessimo with starting value
       // animator = GetComponent<Animator>();
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
        
        animator.SetTrigger("Wave");
        Invoke("Idle", 2.15f);

      if (!rodo)
            {
              pranesimas.gameObject.SetActive(true);
              rodo = true;
            }

      }
    }
 
    private void OnTriggerExit(Collider other)
    {
      if (other.CompareTag("Player")) {
        Debug.Log("EXIT");
        titrai.gameObject.SetActive(false);
        animator.SetTrigger("Wave");
        Invoke("Idle", 2.15f);
        if (chatGPT != null)
        {
            chatGPT.SendReply("Iki greito. Ate.");
        }
        //chatGPT.SendReply("Iki greito. Ate."); 
       // yield return new WaitForSeconds(2.458f);
      }
    }
 
 private void Idle()
    {
        animator.SetTrigger("Idle");
    }

}