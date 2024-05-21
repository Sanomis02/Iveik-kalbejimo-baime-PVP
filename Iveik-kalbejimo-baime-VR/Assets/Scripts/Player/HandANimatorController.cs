using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class HandANimatorController : MonoBehaviour
{
    [SerializeField] private InputActionProperty trigerAction;
    [SerializeField] private InputActionProperty gripAction;

    private Animator animator;

private void Start(){
    animator = GetComponent<Animator>();
}
    private void Update(){
       float triggerValue = trigerAction.action.ReadValue<float>();
       float gripValue = gripAction.action.ReadValue<float>();

        animator.SetFloat("Trigger", triggerValue);
        animator.SetFloat("Grip", gripValue);
    }

}
