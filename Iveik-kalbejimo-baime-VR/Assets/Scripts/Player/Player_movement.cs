using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{

    [SerializeField]
    protected InputTextManager inputManager;

    [Header("Movement")]
    public float greitis;

    public float groundDrag;


    [Header("Ground Check")] //Tikriname ar žaidėjas ant pamato
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    
    public Transform orientacija;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        if(inputManager.getWrite())
            return;
        
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        if(grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientacija.forward * verticalInput + orientacija.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * greitis * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 absGreitis = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(absGreitis.magnitude > greitis)
        {
            Vector3 ribotasGreitis = absGreitis.normalized * greitis;
            rb.velocity = new Vector3(ribotasGreitis.x, rb.velocity.y, ribotasGreitis.z);
        }

    }
}
