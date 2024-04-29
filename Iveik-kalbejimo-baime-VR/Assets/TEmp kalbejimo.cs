using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEmpkalbejimo : MonoBehaviour
{
    private Animator _klabejimoAnim;
    public GameObject vaikas;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        _klabejimoAnim = vaikas.GetComponent<Animator>();
        //_klabejimoAnim.SetBool("Speaking", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _klabejimoAnim.SetBool("Speaking", true);
        }
        //_klabejimoAnim.SetBool("Speaking", true);
    }

    private void OnTriggerExit(Collider other)
    {
        _klabejimoAnim.SetBool("Speaking", false);
    }
}
