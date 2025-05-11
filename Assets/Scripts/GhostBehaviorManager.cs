/// <summary>
/// beheert het gedrag van de geesten en past deze aan.
/// </summary>
public class GhostBehaviorManager
{
    public GhostBehavior CurrentBehavior; 

    public GhostBehaviorManager(GhostBehavior initialBehavior)
    {
        CurrentBehavior = initialBehavior;
    }

    /// <summary>
    /// veranderd het gedrag van de geest.
    /// </summary>
    /// <param name="newBehavior"></param>
    public void SwitchBehavior(GhostBehavior newBehavior)
    {
        if (CurrentBehavior == newBehavior)
            return;

        CurrentBehavior.OnExit();

        CurrentBehavior = newBehavior;

        CurrentBehavior.OnEnter();
    }
}
