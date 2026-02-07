using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int levelNum = 0;
    public bool timePaused;
    void Start()
    {
        Instance = this;
        timePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }

    }

    public void Loadlevel1(Transform player)
    {
        player.position = new Vector3(-6, 3, 0);
    }

}
