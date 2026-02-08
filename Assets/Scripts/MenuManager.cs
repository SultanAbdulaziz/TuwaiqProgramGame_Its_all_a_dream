using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject SettingsUI;
    public GameObject Menu;
    public Slider SensSlider;
    public float sensitivity = 100;
    public bool Audio = true;
    private bool invert = false;
    void Update()
    {
        
    }

    public void Start_ButtonPressed()
    {
        SceneManager.LoadScene("Game");
    }

    public void Settings_ButtonPressed()
    {
        SettingsUI.SetActive(invert);
        invert = !invert;
        Menu.SetActive(invert);
    }
    public void Exit_ButtonPressed()
    {
        Debug.Log("Application is Quitting");
        Application.Quit();
    }

    public void Audio_Toggle()
    {
        Audio = !Audio;
        Debug.Log(Audio);
    }

    public void Sensitivity_Slider()
    {
        sensitivity = SensSlider.value;
        Debug.Log(sensitivity);
    }
}
