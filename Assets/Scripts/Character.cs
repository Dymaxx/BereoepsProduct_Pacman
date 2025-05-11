using UnityEngine;

/// <summary>
/// BaseCharacter is een abstracte klasse. Deze defineert bepaalde acties en eigenschappen die ieder karakter in Pacman heeft. 
/// Onder anderen: Locatie, geluid en of het GameObject actief is. Ieder karakter heeft een AudioSource, Movement en AnimatedSprite nodig.
/// </summary>
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AnimatedSprite))]
public abstract class BaseCharacter : MonoBehaviour
{
    public Movement Movement { get; private set; }
    protected AudioSource AudioSource { get; private set; }
    protected AnimatedSprite AnimatedSprite { get; private set; }

    protected virtual void Awake()
    {
        Movement = GetComponent<Movement>();
        AudioSource = GetComponent<AudioSource>();
        AnimatedSprite = GetComponent<AnimatedSprite>();
    }

    /// <summary>
    /// Wijzigt karakter terug naar originele waardes.
    /// </summary>
    public virtual void ResetState()
    {
        gameObject.SetActive(true);
        Movement.ResetState();
    }

    /// <summary>
    /// Stopt alle bewegingen van een karakter.
    /// </summary>
    public void StopMovement()
    {
        Movement.SetDirection(Vector2.zero);
    }

    /// <summary>
    /// Wijzigt locatie van een karakter.
    /// </summary>
    /// <param name="position">Nieuwe positie</param>
    public void SetPosition(Vector3 position)
    {
        position.z = transform.position.z;
        transform.position = position;
    }

    protected void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayOneShot(clip);
        }
    }

    protected void PlayLoopingSound(AudioClip clip)
    {
        if (clip != null && AudioSource.clip != clip)
        {
            AudioSource.Stop();  // Stop het huidige geluid als het anders is
            AudioSource.clip = clip;
            AudioSource.loop = true;
            AudioSource.Play();  // Speel de nieuwe clip direct af
        }
    }

    protected void StopLoopingSound()
    {
        AudioSource.Stop();
        AudioSource.clip = null;
        AudioSource.loop = false;
    }

    protected void SetAnimation(Sprite[] sprites, bool loop)
    {
        if (AnimatedSprite != null)
        {
            AnimatedSprite.Sprites = sprites;
            AnimatedSprite.loop = loop;
            AnimatedSprite.Restart();
        }
    }
}
