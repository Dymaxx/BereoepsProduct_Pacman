using UnityEngine;

/// <summary>
/// Abstracte basisklasse voor het gedrag van een geest.
/// Elk gedrag erft van deze abstracte klasse.
/// en implementeert zijn eigen versie van het Move-gedrag.
/// </summary>
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

    /// <summary>
    /// Wordt aangeroepen wanneer het gedrag actief wordt.
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// Wordt aangeroepen wanneer het gedrag wordt beëindigd.
    /// </summary>
    public virtual void OnExit() { }

    /// <summary>
    /// Abstracte methode die bepaalt hoe de geest moet bewegen tijdens dit gedrag.
    /// </summary>
    /// <param name="gameObject">Het GameObject waarde geest zich in bevindt.</param>
    public abstract void Move(GameObject GameObject);
}
