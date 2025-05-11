using Assets.Scripts;
using UnityEngine;

public class Ghost : BaseCharacter, IEatable
{
    public static int GhostMultiplier = 1;
    public int Points = 200;

    public GhostBehaviorManager BehaviorManager;
    [SerializeField]
    private GhostBehavior initialBehavior;

    // Behavior configurations
    public GhostFrightened Frightened;
    public GhostChase Chase;
    public GhostScatter Scatter;
    public GhostHome Home;

    public Transform Target;
    public AudioClip MoveSound;

    protected override void Awake()
    {
        base.Awake();
        BehaviorManager = new GhostBehaviorManager(initialBehavior);
    }

    private void Start() 
    {
        ResetState();
        if (initialBehavior is GhostHome)
        {
            Invoke(nameof(SwitchToScatter), Home.Duration);
        }
    } 

    public override void ResetState()
    {
        base.ResetState();

        BehaviorManager.SwitchBehavior(initialBehavior);
    }

    public void PlayGhostMoveSound() => PlaySound(MoveSound);

    public void Eaten()
    {
        ScoreManager.Instance.AddPoints(Points * GhostMultiplier);
        GhostMultiplier += 1;
        if (BehaviorManager.CurrentBehavior is GhostFrightened frightened)
        {
            SetPosition(Home.inside.position);
            frightened.Eaten();
            BehaviorManager.SwitchBehavior(Home);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        HandleMove(collider.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleMove(collision.gameObject);
    }

    private void HandleMove(GameObject gameObject)
    {
        BehaviorManager.CurrentBehavior.Move(gameObject);
    }

    public void SwitchToFrightened()
    {
        if(BehaviorManager.CurrentBehavior is not GhostHome)
        {
            BehaviorManager.SwitchBehavior(Frightened);
            CancelInvoke();
            Invoke(nameof(SwitchToScatter), Frightened.Duration);
        }
    }

    private void SwitchToScatter()
    {
        BehaviorManager.SwitchBehavior(Scatter);
        CancelInvoke();
        Invoke(nameof(SwitchToChase), Scatter.Duration);
    }

    private void SwitchToChase()
    {
        BehaviorManager.SwitchBehavior(Chase);
        CancelInvoke();
        Invoke(nameof(SwitchToScatter), Chase.Duration);
    }
}
