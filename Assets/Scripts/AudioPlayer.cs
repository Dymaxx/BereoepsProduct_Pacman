using UnityEngine;

/// <summary>
/// basisklasse voor het afspelen van geluid.
/// </summary>
public class AudioPlayer : MonoBehaviour
{
    protected AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}