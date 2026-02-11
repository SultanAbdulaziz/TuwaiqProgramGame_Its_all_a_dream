using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int levelNum = 0;
    public bool timePaused;

    [Header("Settings")]
    public bool isAudioOn = true;
    public float sensitivity = 100f;
    public GameObject AudioSource;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

 
    void Start()
    {
        timePaused = false;

        LoadSettings();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timePause();
        }
    }

    public void timePause()
    {
        if (timePaused) AudioManager.Instance.Playresume();
        else AudioManager.Instance.Playpause();
        timePaused = !timePaused;
    }

    public void LevelComplete(Transform player, bool passed)
    {
        if (passed)
        {
            levelNum++;
            AudioManager.Instance.PlayStageWin();
        }

        switch (levelNum)
        {
            case 1: Loadlevel1(player); break;
            case 2: Loadlevel2(player); break;
            case 3: Loadlevel3(player); break;
            case 4: SceneManager.LoadScene("Menu"); Cursor.lockState = CursorLockMode.None; Cursor.visible = true; break;
        }
    }

    public void Loadlevel1(Transform player)
    {
        player.position = new Vector3(-15.5f, 2f, 0f);
    }
    public void Loadlevel2(Transform player)
    {
        player.position = new Vector3(-34f, 2f, 0f);
    }
    public void Loadlevel3(Transform player)
    {
        player.position = new Vector3(-62f, 2f, 0f);
    }

    public void SetAudio(bool value)
    {
        isAudioOn = value;
        ApplyAudio();
        SaveSettings();
    }

    public void SetSensitivity(float value)
    {
        sensitivity = value;
        SaveSettings();
    }

    private void ApplyAudio()
    {
        if (!isAudioOn)
        {
            AudioSource.SetActive(false);
        }
        else
        {
            AudioSource.SetActive(true);
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Audio", isAudioOn ? 1 : 0);
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        isAudioOn = PlayerPrefs.GetInt("Audio", 1) == 1;
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);
        ApplyAudio();
    }
}
