using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpSpeed;
    Rigidbody rb;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    [Header("ESC Related")]
    public GameObject ResumeBlock;
    public GameObject SettingsBlock;
    public GameObject ExitBlock;

    Vector3 moveDirection;
    public Transform Orientation;
    public float horizontalInput;
    public float verticalInput;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        myInput();

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f+0.2f, whatIsGround);
        if (grounded && Input.GetKey(KeyCode.Space))
            rb.AddForce(Vector3.up * jumpSpeed);
    }
    void FixedUpdate()
    {
        Movement();
    }

    private void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    public void Movement()
    {
        moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;
 
        float temp = Math.Min((moveDirection.magnitude * moveSpeed * 10f), 30f);
        if(rb.linearVelocity.magnitude <10) 
        {
            rb.AddForce(moveDirection * temp, ForceMode.Force);
        }
    }

    public void ESCPressed()
    {
        
    }
}
