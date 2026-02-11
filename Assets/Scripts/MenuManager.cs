using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject SettingsUI;
    public GameObject Menu;
    public Slider SensSlider;
    public Toggle AudioToggle;

    private bool invert = false;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            if (AudioToggle != null)
                AudioToggle.isOn = GameManager.Instance.isAudioOn;

            if (SensSlider != null)
                SensSlider.value = GameManager.Instance.sensitivity;
        }
    }

    public void Start_ButtonPressed()
    {
        SceneManager.LoadScene("Game");
    }

    public void Settings_ButtonPressed()
    {
        invert = !invert;
        SettingsUI.SetActive(invert);
        Menu.SetActive(!invert);
    }

    public void Exit_ButtonPressed()
    {
        Debug.Log("Application is Quitting");
        Application.Quit();
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
}
