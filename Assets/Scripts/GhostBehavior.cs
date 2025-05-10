using UnityEngine;

[RequireComponent (typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost Ghost { get; private set; }

    [SerializeField]
    private float duration;

    public float Duration => duration;

    private void Awake()
    {
        Ghost = GetComponent<Ghost>();
    }

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public abstract void Move(GameObject GameObject);
}
