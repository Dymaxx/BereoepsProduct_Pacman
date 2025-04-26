using UnityEngine;

public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }

    [Header("Audio Clips")]
    public AudioClip eatPelletSound;
    public AudioClip deathSound;

    private AudioSource audioSource;
    private bool isDeathSoundPlaying = false;

    public Sprite[] deathSpritesArray;  // Een array van sprites voor de death animatie
    public Sprite[] normalSpritesArray; // Een array van sprites voor de normale animatie

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Geen animatie starten in de Start() om te voorkomen dat de eerste sprite direct wordt aangeroepen
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
        PlayNormalAnimation(); // Speel de normale animatie af bij reset
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
            audioSource.Stop(); // <<<<<< STOP alles wat nu speelt (bijv. waka waka eet geluid)
            isDeathSoundPlaying = true;
            audioSource.PlayOneShot(deathSound);
            Invoke(nameof(ResetDeathSoundFlag), deathSound.length);
        }
    }

    private void ResetDeathSoundFlag()
    {
        isDeathSoundPlaying = false;
    }

    public void StopMovement()
    {
        this.movement.SetDirection(Vector2.zero); // Stop de beweging
    }

    // Methode voor het afspelen van de death animatie
    public void PlayDeathAnimation()
    {
        AnimatedSprite animatedSprite = GetComponent<AnimatedSprite>();

        if (animatedSprite != null)
        {
            animatedSprite.sprites = deathSpritesArray;
            animatedSprite.loop = false;
            animatedSprite.Restart();
        }
        else
        {
            Debug.LogError("No AnimatedSprite component found on " + gameObject.name);
        }
    }

    // Methode voor het afspelen van de normale animatie
    private void PlayNormalAnimation()
    {
        AnimatedSprite animatedSprite = GetComponent<AnimatedSprite>();

        if (animatedSprite != null)
        {
            animatedSprite.sprites = normalSpritesArray; // Stel de normale sprites in
            animatedSprite.loop = true; // Zorg ervoor dat de animatie doorloopt
            animatedSprite.Restart(); // Start de normale animatie vanaf het begin
        }
        else
        {
            Debug.LogError("No AnimatedSprite component found on " + gameObject.name);
        }
    }
}
