using UnityEngine;

/// <summary>
/// Orchestrates game objects when game states change. Like starting, winning or losing the game.
/// Additionally orchestrates powerups that influence other objects available in the GameManager.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Ghost[] ghosts;
    [SerializeField]
    private Pacman pacman;
    [SerializeField]
    private Transform pellets;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (LifeManager.Instance.Lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    public void GameWon()
    {
        pacman.gameObject.SetActive(false);
        Invoke(nameof(NewRound), 3.0f);
    }

    public void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        StopAllGhosts();
        pacman.gameObject.SetActive(false);
    }

    private void NewGame()
    {
        ScoreManager.Instance.ResetScore();
        LifeManager.Instance.ResetLives();
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        GhostAudioManager.Instance.SetMode("scatter");

        ResetState();
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].SwitchToFrightened();

            CancelInvoke();
            Invoke(nameof(ResetAudioToScatter), pellet.Duration);
            Invoke(nameof(ResetGhostMultiplier), pellet.Duration);
        }
    }

    private void ResetState()
    {
        ResetGhostMultiplier();

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
        pacman.PlayEatPelletSound();
    }

    /// <summary>
    /// Scatter audio has to be managed here, Invoke won't trigger in PowerPellet since the object will be disabled after being eaten.
    /// </summary>
    private void ResetAudioToScatter()
    {
        GhostAudioManager.Instance.SetMode("scatter");
    }

    private void ResetGhostMultiplier()
    {
        Ghost.GhostMultiplier = 1;
    }

    private void StopAllGhosts()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].Movement.SetDirection(Vector2.zero);
            ghosts[i].enabled = false;
        }
    }
}
