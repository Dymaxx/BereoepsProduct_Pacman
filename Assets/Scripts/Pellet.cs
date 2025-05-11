using Assets.Scripts;
using UnityEngine;

/// <summary>
/// De pellet zorgt dat je punten krijgt bij opeten. Als alle pellets zijn opgegeten is het spel voorbij.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Pellet : MonoBehaviour, IEatable
{
    private static int pelletCount { get; set; }
    private static int pelletActiveCount { get; set; }

    public int points = 10;

    private void Start()
    {
        pelletCount += 1;
        pelletActiveCount = pelletCount;
    }

    private void OnEnable()
    {
        pelletActiveCount += 1;
    }

    private void OnDisable()
    {
        pelletActiveCount -= 1;
    }

    /// <summary>
    /// Regelt de acties als de pellet is opgegeten. Je krijgt punten en er wordt gecheckt of je het spel hebt gewonnen als alle pellets zijn opgegeten.
    /// </summary>
    public virtual void Eaten()
    {
        gameObject.SetActive(false);
        ScoreManager.Instance.AddPoints(points);

        if (pelletActiveCount <= 0)
        {
            GameManager.Instance.GameWon();
        }
    }
}