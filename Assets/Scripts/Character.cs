using UnityEngine;

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

    public virtual void ResetState()
    {
        gameObject.SetActive(true);
        Movement.ResetState();
    }

    public void StopMovement()
    {
        Movement.SetDirection(Vector2.zero);
    }

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
            AnimatedSprite.sprites = sprites;
            AnimatedSprite.loop = loop;
            AnimatedSprite.Restart();
        }
    }
}
