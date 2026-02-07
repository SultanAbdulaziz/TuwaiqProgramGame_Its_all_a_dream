using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameManager instance;
    public Image BlackScreen;

    public static HUDManager Instance;
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

    public void Death()
    {
        BlackScreen.GetComponent<Animator>().Play("Fade");
    }
}
