using UnityEngine;

public class GhostAudioManager : MonoBehaviour
{
    public static GhostAudioManager Instance { get; private set; }

    [Header("Audio Clips")]
    public AudioClip chaseClip;
    public AudioClip scatterClip;
    public AudioClip frightenedClip;

    private AudioSource audioSource;
    private string currentMode;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void SetMode(string mode)
    {
        if (currentMode == mode)
            return; // Vermijd herhaaldelijk opnieuw afspelen

        currentMode = mode;

        switch (mode)
        {
            case "chase":
                audioSource.clip = chaseClip;
                break;
            case "scatter":
                audioSource.clip = scatterClip;
                break;
            case "frightened":
                audioSource.clip = frightenedClip;
                break;
            default:
                audioSource.clip = null;
                break;
        }

        if (audioSource.clip != null)
            audioSource.Play();
        else
            audioSource.Stop();
    }

    public void StopSound()
    {
        audioSource.Stop();
        currentMode = null;
    }
}
