using UnityEngine;
using UnityEngine.VFX;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource sfxSource;

    public AudioClip teleportClip;
    public AudioClip jumpClip;
    public AudioClip loseClip;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayTeleport()
    {
        sfxSource.PlayOneShot(teleportClip);
    }

    public void PlayJump()
    {
        sfxSource.PlayOneShot(jumpClip);
    }

    public void PlayLose()
    {
        sfxSource.PlayOneShot(loseClip);
    }
}
