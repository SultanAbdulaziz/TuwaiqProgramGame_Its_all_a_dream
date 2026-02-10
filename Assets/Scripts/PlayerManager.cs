using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpSpeed;
    Rigidbody rb;
    public Transform player;
    [Header("Animation")]
    public Animator animator;
    public Animator cameraAnimator;
    public Animator LightAnimator;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;
    [Header("ESC Related")]
    public GameObject ResumeBlock;
    public GameObject SettingsBlock;
    public GameObject ExitBlock;
    private bool ResumePressedOnce;
    private bool SettingsPressedOnce;
    private float distance = 2f;
    public bool timePaused = false;
    Transform cam;

    Vector3 moveDirection;
    public Transform Orientation;
    public float horizontalInput;
    public float verticalInput;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cam = Camera.main.transform;
    }

    private void Update()
    {
        myInput();
        if (Input.GetKeyDown(KeyCode.Escape))
            ESCPressed();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f+0.2f, whatIsGround);
        if (grounded && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpSpeed);
            AudioManager.instance.PlayJump();
            animator.Play("Jump");
        }

    }
    void FixedUpdate()
    {
        if (Math.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Math.Abs(Input.GetAxisRaw("Vertical")) > 0)
            Movement();
        else
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);

        if (Input.GetKey(KeyCode.D))
            animator.Play("Right strafe");
        else if (Input.GetKey(KeyCode.A))
            animator.Play("Left strafe");
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            animator.Play("Walk");
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    private void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    public void Movement()
    {
        Vector3 moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;
        Vector3 currentVelocity = rb.linearVelocity;
        if (new Vector3(currentVelocity.x, 0f, currentVelocity.z).magnitude < 5f)
        {
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed,currentVelocity.y,moveDirection.z * moveSpeed);
            animator.SetBool("isWalk", true);
        }
    }

    public void ESCPressed()
    { 
        Vector3 spawnPos = cam.position + cam.forward * distance;
        Quaternion rot = Quaternion.LookRotation(cam.forward);
        timePaused = !timePaused;
        ResumeBlock.SetActive(timePaused);
        SettingsBlock.SetActive(timePaused);
        ExitBlock.SetActive(timePaused);
        ResumeBlock.GetComponent<Rigidbody>().isKinematic = true;
        SettingsBlock.GetComponent<Rigidbody>().isKinematic = true;
        ExitBlock.GetComponent<Rigidbody>().isKinematic = true;
        ResumeBlock.transform.SetPositionAndRotation(spawnPos + cam.up * .5f, rot);
        SettingsBlock.transform.SetPositionAndRotation(spawnPos , rot);
        ExitBlock.transform.SetPositionAndRotation(spawnPos - cam.up * .5f, rot);
        ResumePressedOnce = false;
        SettingsPressedOnce = false;
        LightAnimator.SetBool("isPause",timePaused);
    }

    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Death":
                HUDManager.Instance.BlackFade();
                AudioManager.instance.PlayLose();
                rb.linearVelocity = new Vector3(0,rb.linearVelocity.y, 0);
                Invoke(nameof(restart), 0.3f);
                break;
            case "Teleporter":
                GameManager.Instance.LevelComplete(player, true);
                AudioManager.instance.PlayTeleport();
                break;
            case "Resume":
                if(ResumePressedOnce) break;
                Invoke(nameof(resume), 0.5f);
                ResumePressedOnce = true;
                break;
            case "Settings":
                if (SettingsPressedOnce) break;
                HUDManager.Instance.openSettingsUI();
                SettingsPressedOnce = true;
                break;
            case "Exit":
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene("Menu");
                break;
        }
    }

    public void resume()
    {
        ESCPressed();
        GameManager.Instance.timePause();
    }
    public void restart()
    {
        GameManager.Instance.LevelComplete(player, false);
    }
    
}
