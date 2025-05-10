public class GhostBehaviorManager
{
    public GhostBehavior CurrentBehavior; 

    public GhostBehaviorManager(GhostBehavior initialBehavior)
    {
        CurrentBehavior = initialBehavior;
    }

    public void SwitchBehavior(GhostBehavior newBehavior)
    {
        if (CurrentBehavior == newBehavior)
            return;

        CurrentBehavior.OnExit();

        CurrentBehavior = newBehavior;

        CurrentBehavior.OnEnter();
    }
}
