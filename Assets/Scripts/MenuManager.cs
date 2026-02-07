using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Update()
    {
        
    }

    public void Start_ButtonPressed()
    {
        SceneManager.LoadScene("Game");
    }

    public void Settings_ButtonPressed()
    {
        //to be added
    }
    public void Exit_ButtonPressed()
    {
        Debug.Log("Application is Quitting");
        Application.Quit();
    }
}
