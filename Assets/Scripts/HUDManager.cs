using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("References")]
    public GameManager instance;      
    public Image BlackScreen;
    public GameObject SettingsUI;
    public Camera playerCamera;
    public TMP_Text interactText;

    [Header("Interaction")]
    public float interactDistance = 4f;

    private GameObject currentObject;
    private Outline currentOutline;

    [Header("Settings UI References")]
    public Toggle audioToggle;
    public Slider sensitivitySlider;

    public TMP_Text deathcounttxt;
    private int deathcount = 0;

    void Start()
    {
        Instance = this;

        BlackScreen.color = new Color(0, 0, 0, 0);

        if (interactText != null)
            interactText.gameObject.SetActive(false);

        if (GameManager.Instance != null)
        {
            if (audioToggle != null)
                audioToggle.isOn = GameManager.Instance.isAudioOn;

            if (sensitivitySlider != null)
                sensitivitySlider.value = GameManager.Instance.sensitivity;
        }
    }

    void Update()
    {
        HandleInteraction();
    }

    void HandleInteraction()
    {
        if (playerCamera == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.green);

            if (hit.collider.CompareTag("Settings") || hit.collider.CompareTag("Resume") || hit.collider.CompareTag("Exit"))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (currentObject != hitObject)
                {
                    ClearInteraction();

                    currentObject = hitObject;

                    currentOutline = currentObject.GetComponentInChildren<Outline>();

                    if (currentOutline != null)
                        currentOutline.enabled = true;

                    if (interactText != null)
                        interactText.gameObject.SetActive(true);
                }

                return;
            }
        }

        ClearInteraction();
    }

    void ClearInteraction()
    {
        if (currentOutline != null)
        {
            currentOutline.enabled = false;
            currentOutline = null;
        }

        currentObject = null;

        if (interactText != null)
            interactText.gameObject.SetActive(false);
    }

    public void BlackFade()
    {
        BlackScreen.GetComponent<Animator>().Play("Fade");
    }

    public void openSettingsUI()
    {
        SettingsUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Update sliders/toggles to match current settings
        if (GameManager.Instance != null)
        {
            if (audioToggle != null)
                audioToggle.isOn = GameManager.Instance.isAudioOn;

            if (sensitivitySlider != null)
                sensitivitySlider.value = GameManager.Instance.sensitivity;
        }
    }

    public void exitSettings()
    {
        SettingsUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Audio_Toggle(bool value)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SetAudio(value);
    }

    public void Sensitivity_Slider(float value)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.SetSensitivity(value);
    }
    private bool wait = false;
    public void waitt()
    {
        wait = !wait;
    }
    public void addDeathtoCount()
    {
        if (!wait)
        {
            deathcounttxt.text = "Deaths : " + (++deathcount); waitt();
            Invoke(nameof(waitt), 1f);
        }
    }
}
