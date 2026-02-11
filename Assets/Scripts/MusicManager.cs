using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public AudioClip themeTrack;
    private AudioSource audioSource;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = themeTrack;
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}