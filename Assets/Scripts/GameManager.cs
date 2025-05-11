using UnityEngine;

/// <summary>
/// Coördineert game-objecten wanneer de spelstatus verandert, zoals bij het starten, winnen of verliezen van het spel.
/// Coördineert daarnaast power-ups die invloed hebben op andere objecten die beschikbaar zijn in de GameManager.
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
    public GameObject myCanvas;

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

    /// <summary>
    /// Regel de acties die de spel moet doen nadat je een scherm hebt overwonnen.
    /// </summary>
    public void GameWon()
    {
        pacman.gameObject.SetActive(false);
        Invoke(nameof(NewRound), 3.0f);
    }

    /// <summary>
    /// Regel de acties die de spel moet doen nadat Pacman al zijn levens kwijt is.
    /// </summary>
    public void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        StopAllGhosts();
        pacman.gameObject.SetActive(false);
        
        myCanvas.SetActive(true);

    }

    private void NewGame()
    {
        myCanvas.SetActive(false);
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

    /// <summary>
    /// Regel de acties die erbij horen nadat een PowerPellet is opgegeten.
    /// </summary>
    /// <param name="pellet">De pellet die is opgegeten, deze bepaald de duur van de powerup.</param>
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
