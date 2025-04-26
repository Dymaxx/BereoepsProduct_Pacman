using UnityEngine;

[RequireComponent (typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost Ghost { get; private set; }
    public float duration;

    private void Awake()
    {
        this.Ghost = GetComponent<Ghost>();

    }

    public void Enable()
    {
        //Call the eneable function with the default duration
        Enable(this.duration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;

        //Cancel invoke if enable is called again
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
    }

}
