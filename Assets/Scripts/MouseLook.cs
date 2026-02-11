using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Camera Rotation")]
    public float sensX = 100f;
    public float sensY = 100f;
    public Transform Orientation;
    float xRotation;
    float yRotation = -90f;

    public GameObject SettingsUI;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (GameManager.Instance != null)
        {
            sensX = GameManager.Instance.sensitivity;
            sensY = GameManager.Instance.sensitivity;
        }
    }

    void Update()
    {
        if (GameManager.Instance != null)
        {
            sensX = GameManager.Instance.sensitivity;
            sensY = GameManager.Instance.sensitivity;
        }

        if (!Input.GetKey(KeyCode.R))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -60f, 60f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            Orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
