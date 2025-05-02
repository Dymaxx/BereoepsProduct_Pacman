using UnityEngine;

public class Ghost : BaseCharacter
{
    public GhostHome Home { get; private set; }
    public GhostScatter Scatter { get; private set; }
    public GhostChase Chase { get; private set; }
    public GhostFrightened Frightened { get; private set; }

    public AudioClip MoveSound;
    public GhostBehavior initialBehavior;
    public Transform target;
    public int points = 200;

    protected override void Awake()
    {
        base.Awake();

        Home = GetComponent<GhostHome>();
        Scatter = GetComponent<GhostScatter>();
        Chase = GetComponent<GhostChase>();
        Frightened = GetComponent<GhostFrightened>();

    }

    private void Start() => ResetState();

    public override void ResetState()
    {
        base.ResetState();

        Frightened.Disable();
        Chase.Disable();
        Scatter.Enable();

        if (Home != initialBehavior)
            Home.Disable();

        initialBehavior?.Enable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            GameManager gameManager = FindFirstObjectByType<GameManager>();

            if (Frightened.enabled)
                gameManager.GhostEaten(this);
            else
                gameManager.PacManEaten();
        }
    }

    public void PlayGhostMoveSound() => PlaySound(MoveSound);
}
