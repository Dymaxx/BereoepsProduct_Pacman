using UnityEngine;

public class Pacman : BaseCharacter
{
    public AudioClip eatPelletSound;
    public AudioClip deathSound;

    private bool isDeathSoundPlaying = false;

    public Sprite[] deathSpritesArray;
    public Sprite[] normalSpritesArray;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            Movement.SetDirection(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            Movement.SetDirection(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            Movement.SetDirection(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            Movement.SetDirection(Vector2.right);

        float angle = Mathf.Atan2(Movement.direction.y, Movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public override void ResetState()
    {
        base.ResetState();
        PlayNormalAnimation();
    }

    public void PlayEatPelletSound() => PlaySound(eatPelletSound);

    public void PlayDeathSound()
    {
        if (!isDeathSoundPlaying && deathSound != null)
        {
            AudioSource.Stop();
            isDeathSoundPlaying = true;
            PlaySound(deathSound);
            Invoke(nameof(ResetDeathSoundFlag), deathSound.length);
        }
    }

    private void ResetDeathSoundFlag() => isDeathSoundPlaying = false;

    public void PlayDeathAnimation() => SetAnimation(deathSpritesArray, false);
    private void PlayNormalAnimation() => SetAnimation(normalSpritesArray, true);
}
