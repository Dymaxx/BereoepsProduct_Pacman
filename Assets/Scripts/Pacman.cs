using Assets.Scripts;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleInteraction(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        HandleInteraction(collider.gameObject);
    }

    private void HandleInteraction(GameObject obj)
    {
        IEatable eatable = obj.GetComponent<IEatable>();
        if (eatable != null)
        {
            Ghost ghost = obj.GetComponent<Ghost>();
            if (ghost != null)
            {
                if (ghost.BehaviorManager.CurrentBehavior is GhostFrightened)
                {
                    ghost.Eaten();
                }
                else
                {
                    PacmanEaten();
                }
            }
            else
            {
                eatable.Eaten();
            }
        }
    }

    private void PacmanEaten()
    {
        StopMovement();

        PlayDeathSound();
        PlayDeathAnimation();

        var soundDuration = deathSound.length;
        Invoke(nameof(Deactivate), soundDuration);

        if (LifeManager.Instance.Lives > 0)
        {
            Invoke(nameof(ResetState), soundDuration + 0.5f);
        }
        else
        {
            GameManager.Instance.Invoke(nameof(GameManager.Instance.GameOver), soundDuration + 0.5f);
        }

        LifeManager.Instance.UpdateLives(LifeManager.Instance.Lives - 1);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void PlayEatPelletSound() => PlayLoopingSound(eatPelletSound);
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
