using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;
    public GameManager instance;
    public Image BlackScreen;
    public GameObject SettingsUI;
    public bool isAudio = true;
    public float sensitivity = 100f;

    void Start()
    {
        Instance = this;
        BlackScreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
            Invoke(nameof(UnPause), 1f);
        }
    }
    public void Pause()
    {
        BlackScreen.GetComponent<Animator>().Play("Pause");
    }
    public void UnPause()
    {
        BlackScreen.GetComponent<Animator>().Play("unPause");
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
    }

    public void exitSettings()
    {
        SettingsUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Audio_Toggle()
    {
        isAudio = !isAudio;
    }

    public void Sensitivity_Slider()
    {
        Slider SensSlider = SettingsUI.GetComponent<Slider>();
        sensitivity = SensSlider.value;
    }
}
