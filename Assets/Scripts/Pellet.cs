using Assets.Scripts;
using UnityEngine;

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