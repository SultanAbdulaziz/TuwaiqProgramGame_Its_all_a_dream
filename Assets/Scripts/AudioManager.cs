using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public static AudioManager instance => Instance;

    [Header("SFX Source")]
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip teleportClip;
    public AudioClip jumpClip;
    public AudioClip loseClip;

    public AudioClip laserClip;
    public AudioClip stageWinClip;
    public AudioClip finalWinClip;
    public AudioClip clickClip;   // ÅÐÇ ÚäÏß ÕæÊ ßáß

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Play(AudioClip clip, float volume = 1f)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayTeleport() => Play(teleportClip);
    public void PlayJump() => Play(jumpClip);
    public void PlayLose() => Play(loseClip);

    public void PlayLaser() => Play(laserClip);
    public void PlayStageWin() => Play(stageWinClip);
    public void PlayFinalWin() => Play(finalWinClip);
    public void PlayClick() => Play(clickClip);
}