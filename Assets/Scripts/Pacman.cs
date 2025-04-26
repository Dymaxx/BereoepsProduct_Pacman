using UnityEngine;

public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }

    [Header("Audio Clips")]
    public AudioClip eatPelletSound;
    public AudioClip deathSound;
    public AudioClip moveSound; // 🔥 Nieuw: loopend geluid toevoegen!

    private AudioSource audioSource;
    private bool isDeathSoundPlaying = false;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMoveSound(); // 🔥 Start loop-geluid als Pacman begint
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movement.SetDirection(Vector2.right);
        }

        float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
        PlayMoveSound(); // 🔥 Speel loop-geluid opnieuw bij respawn
    }

    public void PlayEatPelletSound()
    {
        if (eatPelletSound != null)
        {
            audioSource.PlayOneShot(eatPelletSound);
        }
    }

    public void PlayDeathSound()
    {
        if (!isDeathSoundPlaying && deathSound != null)
        {
            isDeathSoundPlaying = true;
            StopMoveSound(); // 🔥 Stop loop-geluid als hij dood gaat
            audioSource.PlayOneShot(deathSound);
            Invoke(nameof(ResetDeathSoundFlag), deathSound.length);
        }
    }

    private void ResetDeathSoundFlag()
    {
        isDeathSoundPlaying = false;
    }

    public void PlayMoveSound()
    {
        if (moveSound != null)
        {
            audioSource.clip = moveSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopMoveSound()
    {
        if (audioSource.isPlaying && audioSource.clip == moveSound)
        {
            audioSource.Stop();
            audioSource.clip = null;
            audioSource.loop = false;
        }
    }

    public void StopMovement()
    {
        this.movement.SetDirection(Vector2.zero);
        StopMoveSound(); // 🔥 Extra veiligheid: stop loop-geluid als je movement stopt
    }
}
