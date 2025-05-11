using UnityEngine;

/// <summary>
/// Deze klasse zorgt voor het afspelen van geluiden, door middel van het game object waaraan dit script is gekoppeld kun je voor dat specifiek object geluid afspelen.
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