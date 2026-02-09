using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int levelNum = 0;
    public bool timePaused;
    void Start()
    {
        Instance = this;
        timePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timePause();
        }
    }

    public void timePause()
    {
        timePaused = !timePaused;
    }

    public void LevelComplete(Transform player,bool passed)
    {
        if (passed) levelNum++;
        switch (levelNum)
        {
            case 1 : Loadlevel1(player); break;
            case 2 : Loadlevel2(player); break;
            case 3 : Loadlevel3(player); break;
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
        player.position = new Vector3(-15.5f, 2f, 0f);
    }

}
